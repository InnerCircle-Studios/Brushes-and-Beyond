public class PlayerShowState : State
{
    public PlayerShowState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
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
        AddSwitchCase(_PlayerStateMachine._IsShowDone, _PlayerStateMachine.GetState("PlayerIdleState"));
    }

    private PlayerStateMachine _PlayerStateMachine;
}
