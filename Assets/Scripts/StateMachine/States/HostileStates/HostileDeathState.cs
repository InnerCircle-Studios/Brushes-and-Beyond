public class HostileDeathState : State
{
    public HostileDeathState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _HostileStateMachine = GetStateMachine() as HostileStateMachine;
    }

    public override void AwakeState()
    {
        AddSwitchCases();
    }

    public override void EnterState()
    {
        GetStateMachine().GetActor().GetAnimator().Play("Death");
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void AddSwitchCases() 
    {
        
    }

    private HostileStateMachine _HostileStateMachine;
}
