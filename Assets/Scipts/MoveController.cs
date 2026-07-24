using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    public enum MovementState
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Dead



    }
    
    [Header("State Machine")]
    [SerializeField] private MovementState currentState = MovementState.Idle;

    
    
    
    
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Vector2 maxSpeed = new Vector2(5f, 10f);

    [Header("Friction Settings")]
    [Tooltip("Higher numbers mean faster stops when releasing input.")]
    public float decelerationRate = 20f;
    [Tooltip("Turn this on to immediately cancel velocity when changing opposite directions.")]
    public bool snapDirectionChanges = true;

    
    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    private Rigidbody2D rb;

    // Force accumulator - allows external forces to coexist with input movement
    private Vector2 externalForce = Vector2.zero;

    private Vector2 MoveInput;

    private float deathTimer = 0f;
    private const float DEATH_DURATION = 1f;

    public MovementState CurrentState => currentState;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb == null) return;

        //makes it so that if you are going in a direction and press the opposite, you wont slie and drift before going the other way
        if (snapDirectionChanges && MoveInput.x != 0 && Mathf.Sign(MoveInput.x) != Mathf.Sign(rb.linearVelocity.x) && Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }

        //calculates deceleration force based on how fast the player is going currently, slows down player by applying force.
        if (Mathf.Abs(MoveInput.x) < 0.01f && IsGrounded())
        {
            float decelerationForce = -rb.linearVelocity.x * decelerationRate;
            rb.AddForce(new Vector2(decelerationForce, 0f), ForceMode2D.Force);
        }
        
        // Apply horizontal input as acceleration (not velocity override)
        float targetSpeed = MoveInput.x * moveSpeed;
        float speedDiff = targetSpeed - rb.linearVelocity.x;
        rb.AddForce(new Vector2(speedDiff, 0f), ForceMode2D.Force);

        // Apply accumulated external forces (e.g., knockback, wind, etc.)
        if (externalForce != Vector2.zero)
        {
            rb.AddForce(externalForce, ForceMode2D.Impulse);
            externalForce = Vector2.zero; // Reset after applying
        }

        // Clamp velocity to max speeds
        Vector2 clampedVelocity = new Vector2(
            Mathf.Clamp(rb.linearVelocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(rb.linearVelocity.y, -maxSpeed.y, maxSpeed.y)
        );
        rb.linearVelocity = clampedVelocity;
    }

    public void TriggerDeath()
    {
        currentState = MovementState.Dead;
        deathTimer = DEATH_DURATION;
        rb.linearVelocity = Vector2.zero;
    }
    private void DetermineState()
    {
        bool grounded = IsGrounded();

        if (grounded)
        {
            currentState = (Mathf.Abs(MoveInput.x) > 0.01f) ? MovementState.Running : MovementState.Idle;
        }
        else
        {
            currentState = (rb.linearVelocity.y > 0.1f) ? MovementState.Jumping : MovementState.Falling;
        }
    }
    private void Update()
    {
        DetermineState();

        if (currentState == MovementState.Dead)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0f)
            {
                currentState = MovementState.Idle;
            }
            return;
        }
    }

    public void OnRun(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && IsGrounded())
        {
            // Zero vertical velocity first to get consistent jumps
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // External force method - call this from other scripts (knockback, explosions, etc.)
    public void AddExternalForce(Vector2 force, bool isImpulse = false)
    {
        if (isImpulse)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }
        else
        {
            externalForce += force;
        }
    }

    // External force with position for directional knockback
    public void AddExternalForceAtPosition(Vector2 force, Vector2 position, bool isImpulse = true)
    {
        rb.AddForceAtPosition(force, position, isImpulse ? ForceMode2D.Impulse : ForceMode2D.Force);
    }

    private bool IsGrounded()
    {
        if (groundCheck == null) return false;
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    // Properties for external scripts to check state
    public bool IsGroundedPublic => IsGrounded();
    public Vector2 Velocity => rb.linearVelocity;
    public Vector2 MoveInputPublic => MoveInput;
}