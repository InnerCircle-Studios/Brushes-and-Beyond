using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    void AddEvent()
    {
        
    }

    void Update()
    {
        
    }

    public State GetState(string name)
    {
        return _CurrentState;
    }

    public State[] _States;
    public State _CurrentState;
}
