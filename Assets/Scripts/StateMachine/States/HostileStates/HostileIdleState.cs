using UnityEngine;
using System.Collections;

public class HostileIdleState : State
{
    public HostileIdleState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _HostileStateMachine = GetStateMachine() as HostileStateMachine;
    }

    public override void AwakeState()
    {
        AddSwitchCases();
    }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        GetStateMachine().GetActor().GetAnimator().Play(_HostileStateMachine._Colour + "Idle");
        _HostileStateMachine.CheckPlayerInRange();
        CheckSwitchStates();
    }

    public override void ExitState()
    {

    }

    public override void AddSwitchCases() 
    {
         AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isDead, true), _HostileStateMachine.GetState("HostileDeathState"));
         AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isInRange, true), _HostileStateMachine.GetState("HostileWalkState"));
    }

    private HostileStateMachine _HostileStateMachine;
}
