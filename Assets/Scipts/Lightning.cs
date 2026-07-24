using System.Net;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private SceneController sceneController;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private Transform cloud;

    [SerializeField]
    private RodControls rod;

    [SerializeField]
    private LayerMask obstacleLayer;

    [Header("Visual Effects")]
    [SerializeField]
    private CanvasGroup screenFlashCanvasGroup;
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float countdown = 5f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        transform.position = new Vector2(player.position.x, transform.position.y);

        if (timer >= countdown)
        {
            RaycastHit2D ray = Raycast(player.position);

            if (ray.collider == null)
            {
                if (rod.Absorb() == true)
                {
                    CreateLightning(rod.rodTip, Vector2.zero);
                }
                else
                {
                    CreateLightning(player.transform, Vector2.zero);
                    sceneController.die();
                }
            }
            else
            {
                CreateLightning(ray.transform, ray.point);
                Destructable destructable = ray.collider.GetComponent<Destructable>();
                if (destructable != null)
                {
                    destructable.Destruct();
                }
            }
            TriggerScreenEffects();
            timer = 0f;
        }
    }

    private void TriggerScreenEffects()
    {
        // Screen flash
        if (screenFlashCanvasGroup != null)
        {
            StartCoroutine(FlashScreen());
        }

        // Camera shake
        if (mainCamera != null)
        {
            StartCoroutine(ShakeCamera());
        }
    }

    private System.Collections.IEnumerator FlashScreen()
    {
        screenFlashCanvasGroup.alpha = 0.5f;
        yield return new WaitForSeconds(0.1f);
        screenFlashCanvasGroup.alpha = 0f;
    }

    private System.Collections.IEnumerator ShakeCamera()
    {
        Vector3 originalPos = mainCamera.transform.localPosition;
        float shakeDuration = 0.2f;
        float shakeMagnitude = 0.3f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            mainCamera.transform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalPos;
    }

    void FixedUpdate()
    {
        Debug.Log($"Timer: {timer}");
    }

    private RaycastHit2D Raycast(Vector2 startPosition)
    {
        Vector2 targetPosition = new Vector2(cloud.position.x, cloud.position.y);
        Vector2 distance = targetPosition - startPosition;

        RaycastHit2D ray = Physics2D.Raycast(startPosition, distance.normalized, distance.magnitude, obstacleLayer);

        return ray;
    }

    private void CreateLightning(Transform target, Vector2 impactOffset)
    {
        // Create single main bolt with random variations
        int segmentVariation = Random.Range(6, 12);
        float jitterVariation = Random.Range(0.3f, 0.8f);

        GameObject lightning = Instantiate(prefab, prefab.transform.position, Quaternion.identity);
        DrawLightning drawLightning = lightning.GetComponent<DrawLightning>();
        drawLightning.startPoint = cloud;
        drawLightning.endPoint = target;
        drawLightning.segments = segmentVariation;
        drawLightning.jitterAmount = jitterVariation;
        drawLightning.lightningDuration = Random.Range(0.2f, 0.4f); // Vary duration slightly
    }

    public void ExpellLightning(Transform start, Vector2 Dir)
    {
        Vector2 endPosition = (Vector2)start.position + (Dir * 5);
        GameObject endTransform = new GameObject();
        endTransform.transform.position = endPosition;
        endTransform.transform.parent = start;

        // Create single main bolt with random variations
        int segmentVariation = Random.Range(6, 12);
        float jitterVariation = Random.Range(0.3f, 0.8f);
        float lightningDuration = Random.Range(0.2f, 0.4f);

        GameObject lightning = Instantiate(prefab, prefab.transform.position, Quaternion.identity);
        DrawLightning drawLightning = lightning.GetComponent<DrawLightning>();
        drawLightning.startPoint = start;
        drawLightning.endPoint = endTransform.transform;
        drawLightning.segments = segmentVariation;
        drawLightning.jitterAmount = jitterVariation;
        drawLightning.lightningDuration = lightningDuration; // Vary duration slightly
        Destroy(endTransform, lightningDuration);
    }
}