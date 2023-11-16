using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateMachine : StateMachine
{
    public override void Start()
    {
        AddState(new NpcDialogueState("NpcDialogueState", this));
        AddState(new NpcIdleState("NpcIdleState", this));
        AddState(new NpcWalkState("NpcWalkState", this));

        ChangeState(GetState("NpcIdleState"));
    }

}
