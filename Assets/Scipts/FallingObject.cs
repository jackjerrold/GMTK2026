using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float dropTime = 1f;
    //i will be damn suprised if levels go below -40 on the Y axis but just in case.
    public Vector2 endPosition;
    public float respawnTime = 1f;
    public float waitBeforeDrop = 1f;

    private bool hasDroppedYet = false;
    private bool startRespawnTimer = false;
    private Vector2 startPosition;
    private bool objectTouched = false;
    private float elapsedTime = 0f;

    [SerializeField] private AnimationCurve curve;
    private void Start()
    {
        startPosition= transform.position;
    }

    void Update()
    {
        if (startRespawnTimer)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < respawnTime) { return; }
            else
            {
                elapsedTime = 0f;
                startRespawnTimer = false;
                transform.position = startPosition;
                hasDroppedYet = false;
            }

        }

        if (objectTouched)
        {
            elapsedTime += Time.deltaTime;

            if (!hasDroppedYet)
            {
                if (elapsedTime < waitBeforeDrop) { return; } else
                {
                    elapsedTime = 0f;
                    hasDroppedYet=true;
                }
            }

            float percentageComplete = elapsedTime / dropTime;
            Debug.Log($"percentageComplete: {percentageComplete}");
            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percentageComplete));

            if ((Vector2)transform.position == endPosition)
            {
                startRespawnTimer = true;
                objectTouched = false;
                elapsedTime = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //DEATH SHALL COME TO US ALL
        objectTouched = true;
    }
}
