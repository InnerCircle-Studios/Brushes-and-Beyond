using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileStateMachine : StateMachine
{   
    public override void Start()
    {
        AddState(new HostileAttackState("HostileAttackState", this));
        AddState(new HostileDeathState("HostileDeathState", this));
        AddState(new HostileIdleState("HostileIdleState", this));
        AddState(new HostileWalkState("HostileWalkState", this));

        ChangeState(GetState("HostileIdleState"));
    }
}
