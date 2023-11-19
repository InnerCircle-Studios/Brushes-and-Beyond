public class PlayerRunState : State
{
    public PlayerRunState(string name, StateMachine stateMachine) : base(name, stateMachine)
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

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsDeath, true), _PlayerStateMachine.GetState("PlayerDeathState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsMovementPressed, false), _PlayerStateMachine.GetState("PlayerIdleState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsDialogueActive, true), _PlayerStateMachine.GetState("PlayerDialogueState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsRunningPressed, false), _PlayerStateMachine.GetState("PlayerWalkState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsAttackPressed, true), _PlayerStateMachine.GetState("PlayerAttackState"));
    }

    private PlayerStateMachine _PlayerStateMachine;
}
