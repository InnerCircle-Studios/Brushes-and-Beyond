using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
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
        if (Ctx.DialogueTrigger)
        {
            SwitchState(Factory.Dialogue());
        } else
        if (Ctx.IsMovementPressed == false)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsRunningPressed)
        {
            SwitchState(Factory.Run());
        }
        else if (Ctx.IsAttackPressed)
        {
            SwitchState(Factory.Attack());
        }

    }
    public override void InitializeSubStates() { }


    void HandleWalk()
    {
        Vector2 desiredMovement = new Vector2(Ctx.CurrentMovementInput.x, Ctx.CurrentMovementInput.y).normalized * Ctx.Speed * Time.deltaTime;
        Ctx.Rb.MovePosition(Ctx.Rb.position + desiredMovement); ;
    }
    void HandleAnimations()
    {
        switch (Ctx.CurrentDirection)
        {
            case PlayerStateMachine.CharacterDirection.Up:
                Ctx.Animator.Play("BackMove"); // Or "WalkUp", "AttackUp" etc.
                break;
            case PlayerStateMachine.CharacterDirection.Down:
                Ctx.Animator.Play("ForwardMove");
                break;
            case PlayerStateMachine.CharacterDirection.Left:
                Ctx.Animator.Play("SideMove");
                Ctx.SpriteRenderer.flipX = true;
                break;
            case PlayerStateMachine.CharacterDirection.Right:
                Ctx.Animator.Play("SideMove");
                Ctx.SpriteRenderer.flipX = false;
                break;
        }
    }

}
