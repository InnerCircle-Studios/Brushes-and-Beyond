using System.Collections;

using UnityEngine;

public class PaintTubeWalkState : State {
    public PaintTubeWalkState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PaintStateMachine = GetStateMachine() as PaintTubeStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
    }

    public override void EnterState() {
        Move();
        GetStateMachine().GetActor().StartCoroutine(CheckTime());
    }

    public override void UpdateState() {
        if (Vector2.Distance(GetStateMachine().GetActor().transform.position, _targetPosition) < 0.1f) {
            Move();
        }

        MoveToTarget();
        CheckDirection();
        GetStateMachine().GetActor().GetAnimator().Play("Move", _direction);
        _PaintStateMachine.CheckPlayerInRange();
        CheckSwitchStates();
    }

    public override void ExitState() {

    }

    public override void AddSwitchCases() {
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isInRange, false), _PaintStateMachine.GetState("PaintTubeIdleState"));
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isSpawning, true), _PaintStateMachine.GetState("PaintTubeSpawnState"));
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isDead, true), _PaintStateMachine.GetState("PaintTubeDeathState"));
    }

    private void CheckDirection() {
        if (_PaintStateMachine._currentMovement.x != 0) {
            _direction = _PaintStateMachine._currentMovement.x > 0 ? MovementDirection.RIGHT : MovementDirection.LEFT;
            if (_direction == MovementDirection.RIGHT) {
                GetStateMachine().GetActor().GetComponent<SpriteRenderer>().flipX = true;
            }
            else {
                GetStateMachine().GetActor().GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (_PaintStateMachine._currentMovement.y != 0) {
            _direction = _PaintStateMachine._currentMovement.y > 0 ? MovementDirection.UP : MovementDirection.DOWN;
        }
    }

    private IEnumerator CheckTime() {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        _PaintStateMachine._isSpawning.Value = true;
    }

    private void Move() {
        bool _moveOnX = Random.Range(0, 2) == 0;
        float _randomDistance = Random.Range(-10f, 10f);

        if (_moveOnX) {
            _move = new Vector2(_randomDistance, 0);
        }
        else {
            _move = new Vector2(0, _randomDistance);
        }

        _targetPosition = (Vector2)GetStateMachine().GetActor().transform.position + _move;
        _PaintStateMachine._currentMovement = _move.normalized;
    }

    private void MoveToTarget() {
        Transform actorTransform = GetStateMachine().GetActor().transform;
        actorTransform.position = Vector2.MoveTowards(actorTransform.position, _targetPosition, 1f * Time.deltaTime);
    }

    private PaintTubeStateMachine _PaintStateMachine;
    private MovementDirection _direction = MovementDirection.DOWN;

    private Vector2 _move;
    private Vector2 _targetPosition;
}