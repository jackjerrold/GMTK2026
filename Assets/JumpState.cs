using UnityEngine;

public class JumpState : PlayerBaseState
{
    public JumpState(){}
    
    public override void EnterState(PlayerStateManager state)
    {
        Debug.Log("Entered Jump State.");

       
        if (state.jumpCount > 1 && state.rb != null)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, 0f);
            state.rb.AddForce(Vector2.up * state.jumpHeight, ForceMode2D.Impulse);
            Debug.Log("Double Jump Force Applied!");
        }

        // TODO: Play jump animation here
       
    }

    public override void UpdateState(PlayerStateManager state)
    {
       
        if (state.IsGrounded())
        {
            
                state.SwitchState(state.RunState);
            }
            else
            {
                state.SwitchState(state.IdleState);
            }
        }
    

    public override void FixedUpdateState(PlayerStateManager state)
    {
        
        state.rb.linearVelocity = new Vector2(state.MoveInput.x * 5f, state.rb.linearVelocity.y);
    }

   
}
    

