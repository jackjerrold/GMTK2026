using UnityEngine;

public class AirborneState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager state)
    {
        Debug.Log("Entered airborne state. fallin... dreamin...");
        //TODO : Add falling animation
    }
    public override void UpdateState(PlayerStateManager state)
    {
        // 1. Keep checking if the player hits the ground while falling
        if (state.IsGrounded())
        {
            // 2. Transition back to running or idling the frame they land
            if (state.MoveInput.magnitude > 0)
            {
                state.SwitchState(state.RunState);
            } 
            else
            {
                state.SwitchState(state.IdleState);

            } 
        }
    }
    public override void FixedUpdateState(PlayerStateManager state)
    {
        if (state.rb != null)
        {
            state.rb.linearVelocity = new Vector2(state.MoveInput.x * 5f, state.rb.linearVelocity.y);
        }    
    }
    
}
