using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public IdleState IdleState = new IdleState();
    public RunState RunState = new RunState();
    public JumpState JumpState = new JumpState();
    public AirborneState AirborneState = new AirborneState();
    public int jumpCount = 0;
    public int maxJumps = 2;
    public Vector2 MoveInput;
    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;
    public Rigidbody2D rb;



    public void OnRun(InputAction.CallbackContext ctx)
    {
         MoveInput = ctx.ReadValue<Vector2>();

         if (currentState == IdleState && MoveInput.magnitude > 0)
         {
            SwitchState(RunState);
         }
    }
    
    
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if( rb != null && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
            if (jumpCount < maxJumps)
            {
                jumpCount++;
                SwitchState(JumpState);
            }
        }   
    }
     public bool IsGrounded()
    {
      if (groundCheck == null) return false;
      return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }
   public void SwitchState(PlayerBaseState state)

    {
        currentState = state;
        state.EnterState(this);
    }
}

