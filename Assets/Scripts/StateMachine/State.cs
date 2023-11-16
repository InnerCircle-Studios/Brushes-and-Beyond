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

    protected StateMachine GetStateMachine()
    {
        return _StateMachine;
    }

    private string _Name;
    private StateMachine _StateMachine;

}
