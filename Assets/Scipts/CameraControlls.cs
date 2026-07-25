using UnityEngine;

public class CameraControlls : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow")]
    [SerializeField] private float smoothTime = 0.15f;
    [SerializeField] private Vector2 offset;

    [Header("Look Ahead")]
    [SerializeField] private float lookAheadDistance = 2f;
    [SerializeField] private float lookAheadSmooth = 4f;

    [Header("Vertical Look")]
    [SerializeField] private float fallLookOffset = -2f;
    [SerializeField] private float verticalSmooth = 3f;

    private Rigidbody2D targetRB;

    private Vector3 velocity;
    private float currentLookAhead;
    private float currentVerticalOffset;

    private void Awake()
    {
        if (target != null)
            targetRB = target.GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Horizontal look ahead
        float desiredLookAhead = 0;

        if (targetRB != null)
        {
            if (Mathf.Abs(targetRB.linearVelocity.x) > 0.2f)
            {
                desiredLookAhead =
                    Mathf.Sign(targetRB.linearVelocity.x) * lookAheadDistance;
            }
        }

        currentLookAhead = Mathf.Lerp(
            currentLookAhead,
            desiredLookAhead,
            Time.deltaTime * lookAheadSmooth);

        // Vertical look
        float desiredVertical = 0;

        if (targetRB != null)
        {
            if (targetRB.linearVelocity.y < -0.5f)
                desiredVertical = fallLookOffset;
        }

        currentVerticalOffset = Mathf.Lerp(
            currentVerticalOffset,
            desiredVertical,
            Time.deltaTime * verticalSmooth);

        Vector3 targetPosition =
            target.position +
            new Vector3(
                currentLookAhead + offset.x,
                currentVerticalOffset + offset.y,
                -10);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime);
    }
}

