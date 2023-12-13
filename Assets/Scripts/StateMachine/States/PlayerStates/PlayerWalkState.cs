using UnityEngine;

public class PlayerWalkState : State {
    public PlayerWalkState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
    }

    public override void EnterState() {


    }

    public override void UpdateState() {
        HandleWalk();
        CheckSwitchStates();
        _PlayerStateMachine.GetActor().GetAnimator().Play("Move", _PlayerStateMachine._CurrentDirection);
    }

    public override void ExitState() {

    }

    public override void AddSwitchCases() {
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsDeath, true), _PlayerStateMachine.GetState("PlayerDeathState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsInteractPressed, true), _PlayerStateMachine.GetState("PlayerInteractState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsMovementPressed, false), _PlayerStateMachine.GetState("PlayerIdleState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsRunningPressed, true), _PlayerStateMachine.GetState("PlayerRunState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsDialogueActive, true), _PlayerStateMachine.GetState("PlayerDialogueState"));
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsAttackPressed, true), _PlayerStateMachine.GetState("PlayerAttackState"));
    }

    private void HandleWalk() {
        float currentSpeed = _PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes().Speed;
        Vector2 desiresMovement = new Vector2(_PlayerStateMachine._CurrentMovementInput.x, _PlayerStateMachine._CurrentMovementInput.y).normalized * currentSpeed * Time.deltaTime;
        _PlayerStateMachine.GetActor().HandleWalk(desiresMovement);
    }

    private PlayerStateMachine _PlayerStateMachine;
}
