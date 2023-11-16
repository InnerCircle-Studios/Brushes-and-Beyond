using UnityEngine;

public class PlayerWalkState : State
{
    public PlayerWalkState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        HandleWalk();
    }


    private void HandleWalk()
    {
        float currentSpeed = _PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes().Speed;
        Vector2 desiresMovement = new Vector2(_PlayerStateMachine._CurrentMovementInput.x, _PlayerStateMachine._CurrentMovementInput.y).normalized * currentSpeed * Time.deltaTime;
        _PlayerStateMachine.GetActor().HandleWalk(desiresMovement);
    }

    private PlayerStateMachine _PlayerStateMachine;
}
