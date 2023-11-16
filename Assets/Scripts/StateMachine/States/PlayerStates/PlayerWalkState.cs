using UnityEngine;
using UnityEngine.XR;

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
        CharacterAttributes currentAttributes = _PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes();
        Vector2 desiresMovement = new Vector2(_PlayerStateMachine._CurrentMovementInput.x, _PlayerStateMachine._CurrentMovementInput.y).normalized * currentAttributes.Speed * Time.deltaTime;
        _PlayerStateMachine.GetActor().HandleWalk(desiresMovement);
    }

    private PlayerStateMachine _PlayerStateMachine;
}
