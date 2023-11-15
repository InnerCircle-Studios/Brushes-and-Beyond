using System.Collections;
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
        return _CurrentState;
    }

    public State[] _States;
    public State _CurrentState;
}
