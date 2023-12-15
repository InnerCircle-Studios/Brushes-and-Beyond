using System.Collections;

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
         GetStateMachine().GetActor().StartCoroutine(WaitForSitDown());
    }

    public override void UpdateState()
    {
        GetStateMachine().GetActor().GetAnimator().Play("Idle");
        _PaintStateMachine.CheckPlayerInRange();
        CheckSwitchStates();
    } 

    public override void ExitState()
    {
        GetStateMachine().GetActor().StartCoroutine(WaitForStandup());
    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isDead, true), _PaintStateMachine.GetState("PaintTubeDeathState"));
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isInRange, true), _PaintStateMachine.GetState("PaintTubeWalkState"));
    }


    private IEnumerator WaitForSitDown() 
    {
        GetStateMachine().GetActor().GetAnimator().Play("SitDown");
        yield return new WaitForSeconds(1.8f);
    }

    private IEnumerator WaitForStandup()
    {
        GetStateMachine().GetActor().GetAnimator().Play("StandUp");
        yield return new WaitForSeconds(1.8f);
    }
    

    private PaintTubeStateMachine _PaintStateMachine;
}