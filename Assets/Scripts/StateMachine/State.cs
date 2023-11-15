using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    private void EnterState()
    {
        
    }

    private void UpdateState()
    {
        
    }

    private void ExitState()
    {
        
    }

    private void SwitchState(State newstate)
    {

    }

    private string GetName()
    {
        return _Name;
    }

    private string _Name;
    private StateMachine _StateMachine;

}
