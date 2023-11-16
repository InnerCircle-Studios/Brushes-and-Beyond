using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileStateMachine : StateMachine
{   
    public override void Update()
    {
        GetCurrentState().UpdateState();
    }
}
