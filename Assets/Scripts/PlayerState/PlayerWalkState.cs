using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        Debug.Log("PlayerWalkState");
    }
    public override void UpdateState()
    {
        HandleWalk();
        HandleAnimations();
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if (Ctx.IsMovementPressed == false)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsRunningPressed)
        {
            SwitchState(Factory.Run());
        }

    }
    public override void InitializeSubStates() { }


    void HandleWalk()
    {
     Vector2 desiredMovement = new Vector2(Ctx.CurrentMovementInput.x, Ctx.CurrentMovementInput.y).normalized * Ctx.Speed * Time.deltaTime;
    Ctx.Rb.MovePosition(Ctx.Rb.position + desiredMovement);;
    }
void HandleAnimations()
{
    Debug.Log("Attempting to play animation");
    switch(Ctx.CurrentDirection)
    {
        case PlayerStateMachine.CharacterDirection.Up:
            Debug.Log("Up");
            Ctx.Animator.Play("BackIdle"); // Or "WalkUp", "AttackUp" etc.
            break;
        case PlayerStateMachine.CharacterDirection.Down:
            Debug.Log("Down");
            Ctx.Animator.Play("ForwardIdle");
            break;
        case PlayerStateMachine.CharacterDirection.Left:
            Ctx.Animator.Play("SideIdle");
            break;
        case PlayerStateMachine.CharacterDirection.Right:
            Ctx.Animator.Play("SideIdle");
            break;
    }
}

}
