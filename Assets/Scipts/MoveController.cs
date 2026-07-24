using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Vector2 maxSpeed = new Vector2(5f, 10f);

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    private Rigidbody2D rb;

    // Force accumulator - allows external forces to coexist with input movement
    private Vector2 externalForce = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb == null) return;

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