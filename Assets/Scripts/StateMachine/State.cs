using UnityEngine;
using System.Collections.Generic;

public abstract class State
{
    public State(string name, StateMachine stateMachine)
    {
        _Name = name;
        _StateMachine = stateMachine;

        _SwitchStateCases = new Dictionary<SwitchCaseWrapper , State>();
    }

    public abstract void AwakeState();

    public abstract void EnterState();
    
    public abstract void UpdateState();

    public abstract void AddSwitchCases();

    public void ExitState()
    {
        Debug.Log("Exited state: " + _Name);
    }

    public void SwitchState(State newState)
    {
        ExitState();
        _StateMachine.ChangeState(newState);
        newState.EnterState();
    }

    protected void AddSwitchCase(SwitchCaseWrapper boolSwitchCase, State newState)
    {
        _SwitchStateCases.Add(boolSwitchCase , newState);
    }

    protected void CheckSwitchStates()
    {
        foreach (var pair in _SwitchStateCases)
        {
            if (pair.Key._boolWrapper.Value == pair.Key._trueOrFalse)
            {
                SwitchState(pair.Value);
                return;
            }
        }
    }

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
    private Dictionary<SwitchCaseWrapper , State> _SwitchStateCases;
}

public class BoolWrapper
{
    public bool Value { get; set; }

    public BoolWrapper(bool value)
    {
        Value = value;
    }

    public static BoolWrapper operator !(BoolWrapper wrapBool)
    {
        return new BoolWrapper(!wrapBool.Value);
    }
}

public class SwitchCaseWrapper
{
    public SwitchCaseWrapper(BoolWrapper boolWrapper, bool trueOrFalse)
    {
        _boolWrapper = boolWrapper;
        _trueOrFalse = trueOrFalse;
    }

    public BoolWrapper _boolWrapper;
    public bool _trueOrFalse;
}