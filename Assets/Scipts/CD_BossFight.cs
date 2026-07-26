using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CD_BossFight : MonoBehaviour
{
    public float speed;
    public Transform lightingPoint;
    public Transform player;

    [SerializeField]
    private SceneController sceneController;

    [SerializeField]
    private Sprite[] Numbers;

    [SerializeField]
    private Image countdownImage;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private RodControls rod;

    [Header("Visual Effects")]
    [SerializeField]
    private CanvasGroup screenFlashCanvasGroup;
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float countdown = 5f;
    private float timer = 0f;

    private Vector3 startPoint;

    [SerializeField] private int health;

    private void Start()
    {
        startPoint = transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        updateTimer();

        lightingPoint.position = new Vector2(transform.position.x, player.position.y);

        if (timer >= countdown - 1)
        {
            if (rod.BossFightAttack())
            {
                CreateLightning(rod.rodTip);
            }
            else
            {
                CreateLightning(player);
                sceneController.die();
            }

            timer = 0f;
        }
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.right * speed;
    }

    private void CreateLightning(Transform target)
    {

        int segmentVariation = Random.Range(6, 12);
        float jitterVariation = Random.Range(0.3f, 0.8f);

        GameObject lightning = Instantiate(prefab, prefab.transform.position, Quaternion.identity);
        DrawLightning drawLightning = lightning.GetComponent<DrawLightning>();
        drawLightning.startPoint = lightingPoint;
        drawLightning.endPoint = target;
        drawLightning.segments = segmentVariation;
        drawLightning.jitterAmount = jitterVariation;
        drawLightning.lightningDuration = Random.Range(0.2f, 0.4f);
    }

    private void updateTimer()
    {
        int displayTime = (int)Mathf.Round(countdown - timer);

        countdownImage.sprite = Numbers[displayTime - 1];
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
        drawLightning.lightningDuration = lightningDuration;
        Destroy(endTransform, lightningDuration);
    }

    public void Reset()
    {
        transform.position = startPoint;
        timer = 0;
    }
}
