using System.Collections;
using UnityEngine;

public class PlayerShowState : State
{
    public PlayerShowState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void AwakeState()
    {
        AddSwitchCases();
    }

    public override void EnterState()
    {
        GetStateMachine().GetActor().StartCoroutine(WaitForShow());
    }

    public override void ExitState()
    {
        _PlayerStateMachine._IsShowDone.Value = false;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsShowDone, true) , _PlayerStateMachine.GetState("PlayerIdleState"));
    }

    private IEnumerator WaitForShow() //Delay for groundCheck
    {
        yield return new WaitForSeconds(1.8f);

        _PlayerStateMachine._IsShowDone.Value = true;
    }

    private PlayerStateMachine _PlayerStateMachine;
}
