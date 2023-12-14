using UnityEngine;

public class PaintTubeIdleState : State 
{
    public PaintTubeIdleState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _PaintStateMachine = GetStateMachine() as PaintTubeStateMachine;
    }

    public override void AwakeState()
    {
        AddSwitchCases();
    }

    public override void EnterState()
    {
        GetStateMachine().GetActor().GetAnimator().Play("PaintTubeSitDown");
    }

    public override void UpdateState()
    {
        GetStateMachine().GetActor().GetAnimator().Play("PaintTubeIdle");
        _PaintStateMachine.CheckPlayerInRange();
        CheckSwitchStates();
    } 

    public override void ExitState()
    {
        GetStateMachine().GetActor().GetAnimator().Play("PaintTubeStandUp");
    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isDead, true), _PaintStateMachine.GetState("PaintTubeDeathState"));
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isInRange, true), _PaintStateMachine.GetState("PaintTubeWalkState"));
    }

    

    private PaintTubeStateMachine _PaintStateMachine;
}