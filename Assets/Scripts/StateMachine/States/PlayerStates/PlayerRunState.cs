using UnityEngine;

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
        HandleRun();
        _PlayerStateMachine.GetActor().GetAnimator().Play("Sprint",_PlayerStateMachine._CurrentDirection);
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

    private void HandleRun()
    {
        float currentSpeed = _PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes().SprintSpeed;
        Vector2 desiresMovement = new Vector2(_PlayerStateMachine._CurrentMovementInput.x, _PlayerStateMachine._CurrentMovementInput.y).normalized * currentSpeed * Time.deltaTime;
        _PlayerStateMachine.GetActor().HandleWalk(desiresMovement);
    }

    private PlayerStateMachine _PlayerStateMachine;
}
