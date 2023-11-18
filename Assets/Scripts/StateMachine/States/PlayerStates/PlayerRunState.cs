public class PlayerRunState : State
{
    public PlayerRunState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
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
        AddSwitchCase(_PlayerStateMachine._IsDeath, _PlayerStateMachine.GetState("PlayerDeathState"));
        AddSwitchCase(!_PlayerStateMachine._IsMovementPressed, _PlayerStateMachine.GetState("PlayerIdleState"));
        AddSwitchCase(_PlayerStateMachine._IsDialogueActive, _PlayerStateMachine.GetState("PlayerDialogueState"));
        AddSwitchCase(!_PlayerStateMachine._IsRunningPressed, _PlayerStateMachine.GetState("PlayerWalkState"));
        AddSwitchCase(_PlayerStateMachine._IsAttackPressed, _PlayerStateMachine.GetState("PlayerAttackState"));
    }

    private PlayerStateMachine _PlayerStateMachine;
}
