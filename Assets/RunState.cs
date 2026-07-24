using UnityEngine;

public class RunState : PlayerBaseState
{
    public RunState(){}
    
    public override void EnterState(PlayerStateManager state)
    {
        Debug.Log("Entered Run State: we kicking feet");
        //TODO: Play the Run animation
    }
    public override void UpdateState(PlayerStateManager state)
    {
        if (state.MoveInput.magnitude == 0)
        {
            state.SwitchState(state.IdleState);
        }
    }
    public override void FixedUpdateState(PlayerStateManager state)
    {
        
        state.rb.linearVelocity = new Vector2(state.moveSpeed * state.MoveInput.x, state.rb.linearVelocity.y); 
    }
}

