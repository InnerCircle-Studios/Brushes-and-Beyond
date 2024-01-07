using System.Collections;
using UnityEngine;

public class HostileSpawnState : State
{
    public HostileSpawnState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _HostileStateMachine = GetStateMachine() as HostileStateMachine;
    }

    public override void AwakeState()
    {
        AddSwitchCases();
    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        _HostileStateMachine.GetActor().GetAnimator().Play(_HostileStateMachine._Colour +"Spawn");
        _HostileStateMachine.GetActor().StartCoroutine(WaitForAnimation());
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        
    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isDead, true), _HostileStateMachine.GetState("HostileDeathState"));
        AddSwitchCase(new SwitchCaseWrapper(_HostileStateMachine._isSpawned, false), _HostileStateMachine.GetState("HostileIdleState"));
    }

    private IEnumerator WaitForAnimation() 
    {
        float animDuration = GetStateMachine().GetActor().GetAnimator().GetAnimationDuration();
        yield return new WaitForSeconds(animDuration);

        _HostileStateMachine._isSpawned.Value = false;
    }

    private HostileStateMachine _HostileStateMachine;
}