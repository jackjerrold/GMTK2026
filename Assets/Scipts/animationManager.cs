using System.Collections;
using UnityEngine;

public class animationManager : MonoBehaviour
{
    [SerializeField] private MoveController moveController;
    private MoveController.MovementState prevState;
    [SerializeField] private Animator animator;

    private void Update()
    {
        if (moveController.CurrentState != prevState)
        {
            UpdateAnimation(moveController.CurrentState);
        }
        prevState = moveController.CurrentState;
    }

    private void UpdateAnimation(MoveController.MovementState state)
    {
        switch(state)
        {
            case MoveController.MovementState.Idle:
                animator.Play("MC_Idle");
                break;
            case MoveController.MovementState.Running:
                animator.Play("MC_Happy");
                break;
            case MoveController.MovementState.Falling:
                animator.Play("MC_Fall");
                break;
            case MoveController.MovementState.Jumping:
                animator.Play("MC_Jump");
                break;
            case MoveController.MovementState.Dead:
                animator.Play("MC_Zapped");
                break;
        }
    }
}
