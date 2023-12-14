using System.Collections;

using UnityEngine;

public class PaintTubeSpawnState : State
{
    public PaintTubeSpawnState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _PaintStateMachine = GetStateMachine() as PaintTubeStateMachine;
    }

    public override void AwakeState()
    {
        AddSwitchCases();
    }

    public override void EnterState()
    {
        GetStateMachine().GetActor().StartCoroutine(WaitForSpawn());
        GetStateMachine().GetActor().GetAnimator().Play("Spawn");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    } 

    public override void ExitState()
    {

    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isSpawning, false), _PaintStateMachine.GetState("PaintTubeWalkState"));
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isDead, true), _PaintStateMachine.GetState("PaintTubeDeathState"));
    }

    private IEnumerator WaitForSpawn() //Delay for groundCheck
    {
        yield return new WaitForSeconds(1.8f);

        _PaintStateMachine._isSpawning.Value = false;
    }

    private PaintTubeStateMachine _PaintStateMachine;
}