using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawLightning : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public int segments = 5;
    public float jitterAmount = 0.5f;
    private float lightningDuration = 1.0f;

    private LineRenderer lineRenderer;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = segments + 1;


        if (startPoint == null || endPoint == null) return;
        
        GenerateLightning();

        Destroy(gameObject, lightningDuration);
    }


    void Update()
    {

    }

    private void GenerateLightning() {
        lineRenderer.SetPosition(0, startPoint.position);

        
        for (int i = 1; i < segments; i++) {
            float percentage = i / (float) segments; 
            Vector2 pointOnLine = Vector2.Lerp(startPoint.position, endPoint.position, percentage);
            /*
                What this is doing is calculating how much of the line it should be through (percentage)
                Then it uses Lerp() function for Linear Interpolation so finding where the point should be on the line at that percentage completion
                After it will be jittered so shifted from where its supposed to be for randomness
            */

            //Vector2 with x as jitter between +- amount, and y as 0 so it travels straight down.
            Vector2 randomJitter = new Vector2(
                Random.Range(-jitterAmount, jitterAmount),
                0f
                //startPoint.y + yOffset * i
            ); 

            lineRenderer.SetPosition(i, pointOnLine + randomJitter);
        }

        lineRenderer.SetPosition(segments, endPoint.position);
    }
}
