using UnityEngine;

public class IdleState : PlayerBaseState
{
    private PlayerStateManager state;

    public IdleState()
    {
        this.state = state;
    }
    
    
    public override void EnterState(PlayerStateManager state)
    {
        Debug.Log("Entered Idle State: We goin nowhere buddy.");
        // TODO: Add code for idle animations here   
    }
    public override void UpdateState(PlayerStateManager state)
    {
        if (state.MoveInput.magnitude > 0)
        {
            state.SwitchState(state.RunState);
        }   
    }
}
