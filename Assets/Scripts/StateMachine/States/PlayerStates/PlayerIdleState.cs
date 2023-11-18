public class PlayerIdleState : State
{
    public PlayerIdleState(string name, StateMachine stateMachine) : base(name, stateMachine)
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
        AddSwitchCase(_PlayerStateMachine._IsDialogueActive, _PlayerStateMachine.GetState("PlayerDialogueState"));
        AddSwitchCase(_PlayerStateMachine._IsMovementPressed, _PlayerStateMachine.GetState("PlayerWalkState"));
        AddSwitchCase(_PlayerStateMachine._IsAttackPressed, _PlayerStateMachine.GetState("PlayerAttackState"));
        AddSwitchCase(_PlayerStateMachine._IsShowPressed, _PlayerStateMachine.GetState("PlayerShowState"));
    }

    private PlayerStateMachine _PlayerStateMachine; 
}
