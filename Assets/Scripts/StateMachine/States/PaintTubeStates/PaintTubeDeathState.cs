public class PaintTubeDeathState : State 
{
    public PaintTubeDeathState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _PaintStateMachine = GetStateMachine() as PaintTubeStateMachine;
    }

    public override void AwakeState()
    {
        AddSwitchCases();
    }

    public override void EnterState()
    {
        GetStateMachine().GetActor().GetAnimator().Play("PaintTubeDeath");
    }

    public override void UpdateState()
    {
        
    } 

    public override void ExitState()
    {

    }

    public override void AddSwitchCases() 
    {
        
    }

    private PaintTubeStateMachine _PaintStateMachine;
}