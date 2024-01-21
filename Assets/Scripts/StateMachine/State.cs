using System.Collections.Generic;
using System;

[Serializable]
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

    public abstract void ExitState();

    public void SwitchState(State newState)
    {
        ExitState();
        _StateMachine.ChangeState(newState);
        newState.EnterState();
        // Debug.Log("Entering state: " + newState._Name);
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
    public static bool operator ==(BoolWrapper wrapBool1, BoolWrapper wrapBool2) {
        return wrapBool1.Value == wrapBool2.Value;
    }
    public static bool operator !=(BoolWrapper wrapBool1, BoolWrapper wrapBool2) {
        return wrapBool1.Value != wrapBool2.Value;
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        BoolWrapper other = (BoolWrapper)obj;
        return Value == other.Value;
    }

    public override int GetHashCode() {
        return Value.GetHashCode();
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