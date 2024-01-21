using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
    }
    public override void UpdateState()
    {
        HandleRun();
        HandleAnimations();
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if(Ctx.IsAlive == false)
        {
            SwitchState(Factory.Death());
        }
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
            return;
        }
        if (Ctx.DialogueTrigger)
        {
            SwitchState(Factory.Dialogue());
        }

        if (!Ctx.IsRunningPressed)
        {
            SwitchState(Factory.Walk());
            return;
        }

        if (Ctx.IsDashPressed)
        {
            Debug.Log("Dash2");
            SwitchState(Factory.Dash());
            return;
        }

        if (Ctx.IsAttackPressed)
        {
            SwitchState(Factory.Attack());
            return;
        }
    }
    public override void InitializeSubStates() { }

    void HandleRun()
    {
        Vector2 desiredMovement = new Vector2(Ctx.CurrentMovementInput.x, Ctx.CurrentMovementInput.y).normalized * Ctx.SprintSpeed * Time.deltaTime;
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
