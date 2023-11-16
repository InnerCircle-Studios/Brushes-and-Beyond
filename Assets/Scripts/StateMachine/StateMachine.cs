using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    public void AddState(State state)
    {
        _States.Add(state);
    }

    public State GetState(string name)
    {
        foreach(State state in _States)
        {
            if (state.GetName() == name)
            {
                return state;
            }
        }
        return null;
    }

    public void ChangeState(State state)
    {
        _CurrentState = state;
    }

    public State GetCurrentState()
    {
        return _CurrentState;
    }

    public List<State> _States;
    public State _CurrentState;
}
