using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float dropTime = 1f;
    //i will be damn suprised if levels go below -40 on the Y axis but just in case.
    public Vector2 endPosition;

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
        if (objectTouched)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / dropTime;

            transform.position = Vector3.Lerp(transform.position, endPosition, curve.Evaluate(percentageComplete));

            if ((Vector2)transform.position == endPosition)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //DEATH SHALL COME TO US ALL
        objectTouched = true;
    }
}
