using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void EnterState();
    
    public abstract void UpdateState();

    private void ExitState()
    {
        
    }

    private void SwitchState(State newstate)
    {
        ExitState();
        newstate.EnterState();
    }

    private string GetName()
    {
        return _Name;
    }

    private string _Name;
    private StateMachine _StateMachine;

}
