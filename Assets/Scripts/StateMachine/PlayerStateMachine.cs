using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateMachine : StateMachine
{

    public void OnMoveEvent(Vector2 movement)
    {

    }

    public override void Start()
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

    public Vector2 _CurrentMovementInput;
}
