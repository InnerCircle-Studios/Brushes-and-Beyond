using System.Collections;

using UnityEngine;

public class PaintTubeWalkState : State
{
    public PaintTubeWalkState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _PaintStateMachine = GetStateMachine() as PaintTubeStateMachine;
    }

    public override void AwakeState()
    {
        AddSwitchCases();
    }

    public override void EnterState()
    {
        Move();
        GetStateMachine().GetActor().StartCoroutine(CheckTime());
    }

    public override void UpdateState()
    {
        MoveToTarget();
        CheckDirection();
        GetStateMachine().GetActor().GetAnimator().Play("Move", _direction);
        _PaintStateMachine.CheckPlayerInRange();
        CheckSwitchStates();
    } 

    public override void ExitState()
    {

    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isInRange, false), _PaintStateMachine.GetState("PaintTubeIdleState"));
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isSpawning, true), _PaintStateMachine.GetState("PaintTubeSpawnState"));
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isDead, true), _PaintStateMachine.GetState("PaintTubeDeathState"));
    }

    private void CheckDirection()
    {
        _direction = _PaintStateMachine._currentMovement.x > 0 ? MovementDirection.RIGHT : MovementDirection.LEFT;
        _direction = _PaintStateMachine._currentMovement.y > 0 ? MovementDirection.UP : MovementDirection.DOWN;
    }

    private IEnumerator CheckTime()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        _PaintStateMachine._isSpawning.Value = true;
    }

    private void Move()
    {
        bool moveOnX = Random.Range(0,1)==0;
        bool moveNegative = Random.Range(0,1)==0;

        float randomDistance = Random.Range(10f, 10f);

        if (moveOnX)
        {
            if (moveNegative)
            {
                _move = new Vector2(GetStateMachine().GetActor().transform.position.x + -randomDistance, GetStateMachine().GetActor().transform.position.y);
                _PaintStateMachine._currentMovement = new Vector2(-randomDistance, 0);
            }
            else
            {
                _move = new Vector2(GetStateMachine().GetActor().transform.position.x + randomDistance, GetStateMachine().GetActor().transform.position.y);
                _PaintStateMachine._currentMovement = new Vector2(randomDistance, 0);
            }
        }
        else
        {
            if (moveNegative)
            {
                _move = new Vector2(GetStateMachine().GetActor().transform.position.x, GetStateMachine().GetActor().transform.position.y + -randomDistance);
                _PaintStateMachine._currentMovement = new Vector2(0, -randomDistance);
            }
            else
            {
                _move = new Vector2(GetStateMachine().GetActor().transform.position.x, GetStateMachine().GetActor().transform.position.y + randomDistance);
                _PaintStateMachine._currentMovement = new Vector2(0, randomDistance);
            }
        }

    }

    private void MoveToTarget()
    {
        GetStateMachine().GetActor().HandleWalk(_move);
    }

    private PaintTubeStateMachine _PaintStateMachine;
    private MovementDirection _direction = MovementDirection.DOWN;

    private Vector2 _move;
}