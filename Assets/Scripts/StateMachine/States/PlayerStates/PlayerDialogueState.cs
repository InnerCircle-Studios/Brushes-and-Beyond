public class PlayerDialogueState : State
{
    public PlayerDialogueState(string name, StateMachine stateMachine) : base(name, stateMachine)
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
        
    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(!_PlayerStateMachine._IsDialogueActive, _PlayerStateMachine.GetState("PlayerDialogueState"));
    }

    private PlayerStateMachine _PlayerStateMachine; 
}
