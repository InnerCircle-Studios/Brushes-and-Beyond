using System.Collections;

using UnityEngine;

public class PlayerDialogueState : State {
    public PlayerDialogueState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();

    }

    public override void EnterState() {
        _PlayerStateMachine._IsInteractPressed.Value = false; // avoid instant skipping
        _PlayerStateMachine.GetActor().GetAnimator().Play("Idle",_PlayerStateMachine._CurrentDirection);
    }

    public override void ExitState() {
        _PlayerStateMachine._IsInteractPressed.Value = false; // To avoid triggering the dialogue again when exiting the state.
    }

    public override void UpdateState() {
        CheckSwitchStates();

        if (_PlayerStateMachine._IsInteractPressed.Value && !hasCooldown && letInteractGo) {
           GameManager.Instance.GetDialogueManager().NextEntry();
            _PlayerStateMachine.GetActor().StartCoroutine(Cooldown());
            letInteractGo = false; // Avoids skipping the dialogue when the player holds the interact button.
        }
        
        // reset letInteractGo when the player stops holding the interact button.
        if(!_PlayerStateMachine._IsInteractPressed.Value && !letInteractGo){
            letInteractGo = true;
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
    private bool hasCooldown;
    private bool letInteractGo; // If false, the player is holding the interact button.

}