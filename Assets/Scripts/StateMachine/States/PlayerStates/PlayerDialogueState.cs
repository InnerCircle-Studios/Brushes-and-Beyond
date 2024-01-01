using System.Collections;

using UnityEngine;

public class PlayerDialogueState : State {
    private bool hasCooldown;
    public PlayerDialogueState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();

    }

    public override void EnterState() {
        _PlayerStateMachine._IsInteractPressed.Value = false;
    }

    public override void ExitState() {
        _PlayerStateMachine._IsInteractPressed.Value = false;
    }

    public override void UpdateState() {
        CheckSwitchStates();

        if (_PlayerStateMachine._IsInteractPressed.Value && !hasCooldown) {
            _PlayerStateMachine.GetActor().GetGameManager().GetDialogueManager().NextEntry();
            _PlayerStateMachine.GetActor().StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown() {
        hasCooldown = true;
        yield return new WaitForSecondsRealtime(0.1f);
        hasCooldown = false;
    }



    public override void AddSwitchCases() {
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsDialogueActive, false), _PlayerStateMachine.GetState("PlayerIdleState"));
    }

    private PlayerStateMachine _PlayerStateMachine;
}