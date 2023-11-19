using UnityEngine;

public class PlayerIdleState : State
{
    public PlayerIdleState(string name, StateMachine stateMachine) : base(name, stateMachine)
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
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsDeath, true), _PlayerStateMachine.GetState("PlayerDeathState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsDialogueActive, true) , _PlayerStateMachine.GetState("PlayerDialogueState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsMovementPressed, true) , _PlayerStateMachine.GetState("PlayerWalkState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsAttackPressed, true) , _PlayerStateMachine.GetState("PlayerAttackState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsShowPressed, true), _PlayerStateMachine.GetState("PlayerShowState"));
    }

    private PlayerStateMachine _PlayerStateMachine; 
}
