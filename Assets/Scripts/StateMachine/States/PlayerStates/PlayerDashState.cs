public class PlayerDashState : State
{
    public PlayerDashState(string name, StateMachine stateMachine) : base(name, stateMachine)
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
        
    }
}