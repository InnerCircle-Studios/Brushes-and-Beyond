using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        Debug.Log("PlayerIdleState");
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        // When player is Idle and Jump is pressed, switched to jump state
        if (Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializeSubStates() { }
}
