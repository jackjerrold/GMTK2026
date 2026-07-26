using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawLightning : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private Vector2 endPointPosition;

    [Header("Lightning Appearance")]
    public int segments = 8;
    public float jitterAmount = 0.5f;
    public float branchChance = 0.3f;
    public int maxDepth = 2;

    [Header("Animation")]
    [SerializeField]
    public float lightningDuration = 0.3f;
    public float flickerSpeed = 30f;
    public float flickerIntensity = 0.5f;
    public AnimationCurve widthCurve;

    private LineRenderer lineRenderer;
    private Material lightningMaterial;
    private float elapsedTime = 0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lightningMaterial = lineRenderer.material;

        lineRenderer.positionCount = segments + 1;

        if (startPoint == null || endPoint == null)
        {
            Destroy(gameObject);
            return;
        }

        endPointPosition = endPoint.position;

        GenerateLightning(Vector2.zero, Vector2.zero, 0, maxDepth);

        Destroy(gameObject, lightningDuration);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (endPoint != null)
        {
            endPointPosition = endPoint.position;
        }

        AnimateLightning();
    }

    private void AnimateLightning()
    {
        // Flicker effect - animate the jitter for shimmering
        for (int i = 1; i < segments; i++)
        {
            float percentage = i / (float)segments;
            Vector2 pointOnLine = Vector2.Lerp(startPoint.position, endPointPosition, percentage);

            // Subtle flickering offset
            float flickerOffset = Mathf.Sin(elapsedTime * flickerSpeed + i) * 0.1f * flickerIntensity;

            Vector2 randomJitter = new Vector2(
                Random.Range(-jitterAmount, jitterAmount) + flickerOffset,
                Random.Range(-jitterAmount * 0.2f, jitterAmount * 0.2f)
            );

            lineRenderer.SetPosition(i, pointOnLine + randomJitter);
        }

        // Animate line width for electric effect
        float intensity = Mathf.Sin(elapsedTime * flickerSpeed * 2f) * 0.5f + 0.5f;
        lineRenderer.startWidth = Mathf.Lerp(0.15f, 0.35f, intensity);
        lineRenderer.endWidth = Mathf.Lerp(0.1f, 0.25f, intensity);

        // Animate color for flickering brightness
        Color tint = new Color(
            0.8f + 0.2f * Mathf.Sin(elapsedTime * flickerSpeed * 1.5f),
            0.9f + 0.1f * Mathf.Cos(elapsedTime * flickerSpeed * 1.3f),
            1f,
            1f - elapsedTime / lightningDuration // Fade out
        );
        lineRenderer.startColor = tint;
        lineRenderer.endColor = tint;
    }

    private void GenerateLightning(Vector2 directionOffset, Vector2 positionOffset, int currentDepth, int maxDepth)
    {
        lineRenderer.SetPosition(0, (Vector2)startPoint.position + positionOffset);

        for (int i = 1; i < segments; i++)
        {
            float percentage = i / (float)segments;
            Vector2 pointOnLine = Vector2.Lerp(startPoint.position, endPoint.position, percentage);

            Vector2 randomJitter = new Vector2(
                Random.Range(-jitterAmount, jitterAmount),
                Random.Range(-jitterAmount * 0.3f, jitterAmount * 0.3f) // Add slight vertical variation
            );

            lineRenderer.SetPosition(i, pointOnLine + randomJitter + positionOffset);

            // Chance to create a branch
            if (currentDepth < maxDepth && Random.value < branchChance)
            {
                CreateBranch(pointOnLine + randomJitter + positionOffset, percentage, currentDepth + 1);
            }
        }

        lineRenderer.SetPosition(segments, (Vector2)endPoint.position + positionOffset);
    }

    private void CreateBranch(Vector2 branchStart, float percentageAlongBolt, int depth)
    {
        // Create a smaller branch going off at an angle
        int branchSegments = Mathf.Max(2, segments / 2);
        float branchJitter = jitterAmount * 0.5f;

        GameObject branchObj = new GameObject("LightningBranch");
        branchObj.transform.parent = transform;
        branchObj.transform.position = branchStart;

        LineRenderer branchRenderer = branchObj.AddComponent<LineRenderer>();
        branchRenderer.material = lightningMaterial;
        branchRenderer.positionCount = branchSegments + 1;
        branchRenderer.startWidth = lineRenderer.startWidth * 0.5f;
        branchRenderer.endWidth = lineRenderer.endWidth * 0.5f;

        // Branch direction - perpendicular to main bolt direction
        Vector2 mainDirection = (endPoint.position - startPoint.position).normalized;
        Vector2 branchDirection = new Vector2(-mainDirection.y, mainDirection.x) * Random.Range(0.5f, 1.5f);

        for (int i = 0; i <= branchSegments; i++)
        {
            float t = (float)i / branchSegments;
            Vector2 branchEnd = branchStart + branchDirection * (percentageAlongBolt * 0.5f);
            Vector2 point = Vector2.Lerp(branchStart, branchEnd, t);
            Vector2 jitter = new Vector2(
                Random.Range(-branchJitter, branchJitter),
                Random.Range(-branchJitter, branchJitter)
            );
            branchRenderer.SetPosition(i, point + jitter);
        }

        // Auto-destroy branch
        Destroy(branchObj, lightningDuration * 0.8f);
    }
}