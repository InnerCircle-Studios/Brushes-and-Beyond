public class PlayerShowState : State
{
    public PlayerShowState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
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
        CheckSwitchStates();
    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsShowDone, true) , _PlayerStateMachine.GetState("PlayerIdleState"));
    }

    private PlayerStateMachine _PlayerStateMachine;
}
