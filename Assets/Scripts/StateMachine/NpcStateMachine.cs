using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateMachine : StateMachine
{
    public override void Update()
    {
        GetCurrentState().UpdateState();
    }

}
