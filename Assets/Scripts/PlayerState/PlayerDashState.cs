using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{

    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    private Vector2 _dashDirection;
    private float _dashEndTime;

    public override void EnterState()
    {
        Debug.Log("PlayerDashState");
        _dashEndTime = Time.time + Ctx.DashDuration;
        HandleAnimations();
    }

    public override void UpdateState()
    {
        HandleDash();
        HandleAnimations();
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates()
    {

        if (Time.time > _dashEndTime)
        {
            Ctx.Rb.velocity = Vector2.zero;
            if (Ctx.IsMovementPressed)
            {
                if (Ctx.IsRunningPressed)
                {
                    SwitchState(Factory.Run());
                }
                else
                {
                    SwitchState(Factory.Walk());
                }
            }
            else
            {
                SwitchState(Factory.Idle());
            }
        }


    }
    public override void InitializeSubStates() { }


    public void HandleDash()
    {
        if (Time.time - Ctx.LastDashTime >= Ctx.DashCooldown)
        {
            _dashDirection = new Vector2(Ctx.CurrentMovementInput.x, Ctx.CurrentMovementInput.y).normalized;
            Ctx.Rb.velocity = _dashDirection * (Ctx.DashDistance / Ctx.DashDuration);
            Ctx.LastDashTime = Time.time;
        }
    }
    public void HandleAnimations() { }


}