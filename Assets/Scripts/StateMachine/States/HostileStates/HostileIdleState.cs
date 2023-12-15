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
        GetStateMachine().GetActor().GetAnimator().Play("Idle");
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
         AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isDead, true), _HostileStateMachine.GetState("HostileDeathState"));
         AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isInRange, true), _HostileStateMachine.GetState("HostileWalkState"));
    }

    public override void AddSwitchCases() 
    {
        
    }

    private HostileStateMachine _HostileStateMachine;
}
