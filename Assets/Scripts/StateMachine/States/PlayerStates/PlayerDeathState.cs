public class PlayerDeathState : State
{
    public PlayerDeathState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {

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
        
    }
}
