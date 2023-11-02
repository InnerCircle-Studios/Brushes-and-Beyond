using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
    }
    public override void UpdateState()
    {
        HandleAnimations();
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if (Ctx.DialogueTrigger)
        {
            SwitchState(Factory.Dialogue());
        } else if (Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsAttackPressed)
        {
            SwitchState(Factory.Attack());
        }
        else if (Ctx.IsShowPressed){
            SwitchState(Factory.Show());
        }
    }
    public override void InitializeSubStates() { }

    void HandleAnimations()
    {
        switch (Ctx.CurrentDirection)
        {
            case PlayerStateMachine.CharacterDirection.Up:
                Ctx.Animator.Play("BackIdle"); // Or "WalkUp", "AttackUp" etc.
                break;
            case PlayerStateMachine.CharacterDirection.Down:
                Ctx.Animator.Play("ForwardIdle");
                break;
            case PlayerStateMachine.CharacterDirection.Left:
                Ctx.Animator.Play("SideIdle");
                Ctx.SpriteRenderer.flipX = true;
                break;
            case PlayerStateMachine.CharacterDirection.Right:
                Ctx.Animator.Play("SideIdle");
                Ctx.SpriteRenderer.flipX = false;
                break;
        }
    }
}
