using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public State(string name, StateMachine stateMachine)
    {
        _Name = name;
        _StateMachine = stateMachine;
    }

    public abstract void EnterState();
    
    public abstract void UpdateState();

    public void ExitState()
    {
        
    }

    public void SwitchState(State newstate)
    {
        ExitState();
        newstate.EnterState();
    }

    public string GetName()
    {
        return _Name;
    }

    private string _Name;
    private StateMachine _StateMachine;

}
