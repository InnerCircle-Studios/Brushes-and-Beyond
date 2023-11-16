using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerStateMachine(Actor actor) : base(actor)
    {
       AddState(new PlayerAttackState("PlayerAttackState", this));
       AddState(new PlayerDashState("PlayerDashState", this));
       AddState(new PlayerDeathState("PlayerDeathState", this));
       AddState(new PlayerDialogueState("PlayerDialogueState", this));
       AddState(new PlayerIdleState("PlayerIdleState", this));
       AddState(new PlayerRunState("PlayerRunState", this));
       AddState(new PlayerShowState("PlayerShowState", this));
       AddState(new PlayerWalkState("PlayerWalkState", this));

       ChangeState(GetState("PlayerIdleState"));
    }

    public void OnMoveEvent(Vector2 movement)
    {

    }

    public Vector2 _CurrentMovementInput;
}
