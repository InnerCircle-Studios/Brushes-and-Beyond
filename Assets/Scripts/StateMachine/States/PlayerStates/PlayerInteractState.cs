using System.Linq;

using UnityEngine;

public class PlayerInteractState : State {

    public PlayerInteractState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
    }

    public override void EnterState() {
        CheckInteractions();
    }

    public override void ExitState() {

    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void AddSwitchCases() {
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsDialogueActive, true), _PlayerStateMachine.GetState("PlayerDialogueState"));

        // need to check if the interact button has been released before switching back to idle to prevent the player from interacting with the same object multiple times when holding the interact key.
        // btw these boolwrappers are annoying. 
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsInteractPressed, false), _PlayerStateMachine.GetState("PlayerIdleState"));
    }

    private void CheckInteractions() {
        Player myplayer = (Player)_PlayerStateMachine.GetActor();
        Interactable closestInteractable = myplayer.GetClosestInteractable();
        if (closestInteractable != null && !closestInteractable.GetAutoTrigger()) {
            closestInteractable.OnEventTrigger.Invoke();
        }
    }



    private PlayerStateMachine _PlayerStateMachine;
}

// Copilot wants me to write this:
/*
I am going to write a wrapper
a wrapper that wraps the wrapper that wraps the wrapper that wraps the wrapper that wraps the wrapper that wraps the wrapper that wraps the wrapper that wraps the wrapper.
I had a wrapper once
they made a wrapper for my wrapper
a wrapper wrapper
a wrapper wrapper for the wrapper
wrappers make me crazy
crazy?
I was crazy once.
*/