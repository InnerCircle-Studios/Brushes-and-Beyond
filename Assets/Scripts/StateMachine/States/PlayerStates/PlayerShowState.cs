using System.Collections;

using UnityEngine;

public class PlayerShowState : State {
    public PlayerShowState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
        windowManager = _PlayerStateMachine.GetActor().GetGameManager().GetWindowManager();
    }

    public override void EnterState() {
        GetStateMachine().GetActor().StartCoroutine(WaitForShow());
        _PlayerStateMachine.GetActor().GetAnimator().Play("ShowBrush");
        windowManager.InitDialogueBox(_PlayerStateMachine.GetActor());
        windowManager.UpdateDialogueBox("Test string. Blood for the blood god! Skulls for the skull throne!");
    }

    public override void ExitState() {
        _PlayerStateMachine._IsShowDone.Value = false;
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void AddSwitchCases() {
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsShowDone, true), _PlayerStateMachine.GetState("PlayerIdleState"));
    }

    private IEnumerator WaitForShow() {
        yield return new WaitForSeconds(1.8f);

        _PlayerStateMachine._IsShowDone.Value = true;
    }

    private PlayerStateMachine _PlayerStateMachine;
    private WindowManager windowManager;

}
