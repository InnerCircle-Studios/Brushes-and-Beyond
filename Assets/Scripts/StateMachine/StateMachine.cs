using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public void AddEvent()
    {
        
    }

    public abstract void Update();

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

    public List<State> _States;
    public State _CurrentState;
}
