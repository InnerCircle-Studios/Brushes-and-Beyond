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
        GetStateMachine().GetActor().GetAnimator().Play("Move");
        _HostileStateMachine.CheckPlayerInRange();
        MoveToTarget();
        CheckDirection();
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

            _move = Vector2.MoveTowards(GetStateMachine().GetActor().transform.position, _HostileStateMachine._Hostile.GetPlayer().transform.position, _moveSpeed * Time.deltaTime);
        }
        else {
            _HostileStateMachine.CheckPlayerInAttackRange();
        }
        GetStateMachine().GetActor().transform.position = _move;
        _HostileStateMachine._currentMovement = _move.normalized;
    }


    private void CheckDirection() {
        Vector2 currentPosition = GetStateMachine().GetActor().transform.position;
        Vector2 targetPosition = _HostileStateMachine._Hostile.GetPlayer().transform.position;
        if (targetPosition.x > currentPosition.x) {
            _direction = MovementDirection.RIGHT;
            GetStateMachine().GetActor().GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (targetPosition.x < currentPosition.x) {
            _direction = MovementDirection.LEFT;
            GetStateMachine().GetActor().GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    private float _moveSpeed = 1.5f;
    private float _distanceToPlayer;
    private MovementDirection _direction = MovementDirection.DOWN;
    private HostileStateMachine _HostileStateMachine;
    private Vector2 _move;
}
