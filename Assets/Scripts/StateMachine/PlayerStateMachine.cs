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
       GetCurrentState().UpdateState(); 
    }

    public Vector2 _CurrentMovementInput;
}
