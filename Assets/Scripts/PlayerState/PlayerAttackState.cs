using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    private bool _waitOver = false;
    
    public override void EnterState()
    {
        HandleAnimations();
        Ctx.StartCoroutine(WaitForAttack());
        Debug.Log("PlayerAttackState"); ;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if (_waitOver){
            _waitOver = false;
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializeSubStates() { }

    void HandleAnimations()
    {
        switch (Ctx.CurrentDirection)
        {
            case PlayerStateMachine.CharacterDirection.Up:
                Ctx.Animator.Play("BackSwordSwing"); // Or "WalkUp", "AttackUp" etc.
                break;
            case PlayerStateMachine.CharacterDirection.Down:
                Ctx.Animator.Play("ForwardSwordSwing");
                break;
            case PlayerStateMachine.CharacterDirection.Left:
                Ctx.Animator.Play("SideSwordSwing");
                Ctx.SpriteRenderer.flipX = true;
                break;
            case PlayerStateMachine.CharacterDirection.Right:
                Ctx.Animator.Play("SideSwordSwing");
                Ctx.SpriteRenderer.flipX = false;
                break;
        }
    }

    private IEnumerator WaitForAttack() //Delay for groundCheck
    {
        yield return new WaitForSeconds(0.5f);

        _waitOver = true;
    }
}
