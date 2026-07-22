using UnityEngine;
using UnityEngine.InputSystem;


public class MoveController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    public Vector2 MoveInput;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();  
    }

    public void OnRun(InputAction.CallbackContext ctx)
    {

        MoveInput = ctx.ReadValue<Vector2>();

        
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && rb != null && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
      if (groundCheck == null) return false;
      return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
      

    // Update is called once per frame
    void FixedUpdate()
    {
      if (rb != null)
      {
        rb.linearVelocity = new Vector2(MoveInput.x * moveSpeed, rb.linearVelocity.y);
      }

    }
}
