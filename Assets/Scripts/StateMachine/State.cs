using UnityEngine;
using System.Collections.Generic;

public abstract class State
{
    public State(string name, StateMachine stateMachine)
    {
        _Name = name;
        _StateMachine = stateMachine;

        _SwitchStateCases = new Dictionary<bool , State>();
    }

    public abstract void AwakeState();

    public abstract void EnterState();
    
    public abstract void UpdateState();

    public void ExitState()
    {
        Debug.Log("Exited state: " + _Name);
    }

    public void SwitchState(State newstate)
    {
        ExitState();
        newstate.EnterState();
    }

    protected void AddSwitchCase(bool boolSwitchCase, State newState)
    {
        _SwitchStateCases.Add(boolSwitchCase , newState);
    }

    protected void CheckSwitchStates()
    {
        foreach (var pair in _SwitchStateCases)
        {
            if (pair.Key)
            {
                SwitchState(pair.Value);
            }
        }
    }

    public abstract void AddSwitchCases();

    public string GetName()
    {
        return _Name;
    }

    protected StateMachine GetStateMachine()
    {
        return _StateMachine;
    }

    private string _Name;
    private StateMachine _StateMachine;
    private Dictionary<bool , State> _SwitchStateCases;
}