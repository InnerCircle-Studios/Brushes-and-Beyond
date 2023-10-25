using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShowState : PlayerBaseState
{
    public PlayerShowState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    private bool _waitOver = false;

    public override void EnterState()
    {
        HandleAnimations();
        Ctx.StartCoroutine(WaitForShow());
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if (_waitOver)
        {
            _waitOver = false;
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializeSubStates() { }

    void HandleAnimations()
    {
        Ctx.Animator.Play("ShowBrush");
    }

    private IEnumerator WaitForShow() //Delay for groundCheck
    {
        yield return new WaitForSeconds(1.8f);

        _waitOver = true;
    }
}
