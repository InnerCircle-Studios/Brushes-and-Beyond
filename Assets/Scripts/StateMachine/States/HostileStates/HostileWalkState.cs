using UnityEngine;

public class HostileWalkState : State {
    public HostileWalkState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _HostileStateMachine = GetStateMachine() as HostileStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
    }

    public override void EnterState() {

    }

    public override void UpdateState() {
        CheckDirection();
        GetStateMachine().GetActor().GetAnimator().Play("Move");
        _HostileStateMachine.CheckPlayerInRange();
        MoveToTarget();
        CheckSwitchStates();
    }

    public override void ExitState() {

    }

    public override void AddSwitchCases() {
        AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isDead, true), _HostileStateMachine.GetState("HostileDeathState"));
        AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isInAttackRange, true), _HostileStateMachine.GetState("HostileAttackState"));
        AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isInRange, false), _HostileStateMachine.GetState("HostileIdleState"));
    }

    private void MoveToTarget() {
        _distanceToPlayer = Vector2.Distance(GetStateMachine().GetActor().transform.position, _HostileStateMachine._Hostile.GetPlayer().transform.position);

        if (_distanceToPlayer > _HostileStateMachine._attackRange) {
            GetStateMachine().GetActor().transform.position = Vector2.MoveTowards(GetStateMachine().GetActor().transform.position, _HostileStateMachine._Hostile.GetPlayer().transform.position, _moveSpeed * Time.deltaTime);
        }
        else {
            _HostileStateMachine.CheckPlayerInAttackRange();
        }
    }


    private void CheckDirection() {
        _direction = _HostileStateMachine._currentMovement.x > 0 ? MovementDirection.RIGHT : MovementDirection.LEFT;
        if (_direction == MovementDirection.RIGHT) {
            GetStateMachine().GetActor().GetComponent<SpriteRenderer>().flipX = true;
        }
        else {
            GetStateMachine().GetActor().GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private float _moveSpeed = 1.5f;
    private float _distanceToPlayer;
    private MovementDirection _direction = MovementDirection.DOWN;
    private HostileStateMachine _HostileStateMachine;
}
