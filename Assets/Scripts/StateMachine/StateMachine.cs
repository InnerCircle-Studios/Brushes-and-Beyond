using System.Collections.Generic;

public abstract class StateMachine
{
    public StateMachine(Actor actor)
    {
        _Actor = actor;

        _eventManager = EventManager.GetEventManager();

        _States = new List<State>();
    }

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

    protected void InitSwitchCases()
    {
        foreach (State state in _States)
        {
            state.AwakeState();
        }
    }

    public void ChangeState(State state)
    {
        _CurrentState = state;
    }

    public State GetCurrentState()
    {
        return _CurrentState;
    }

    public Actor GetActor()
    {
        return _Actor;
    }

    protected EventManager GetEventManager()
    {
        return _eventManager;
    }

    private List<State> _States;
    private State _CurrentState;
    private Actor _Actor;
    private EventManager _eventManager;
}
