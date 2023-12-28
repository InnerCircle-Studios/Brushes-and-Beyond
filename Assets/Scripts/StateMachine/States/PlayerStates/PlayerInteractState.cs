using System.Linq;

using UnityEngine;

public class PlayerInteractState : State {
    private float interactionRange;

    public PlayerInteractState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
        interactionRange = _PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes().InteractionRange;
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

        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsDialogueActive, false), _PlayerStateMachine.GetState("PlayerIdleState"));
    }

    private void CheckInteractions() {
        Player myplayer = (Player)_PlayerStateMachine.GetActor();
        Interactable closestInteractable = myplayer.GetClosestInteractable();
        if (closestInteractable != null) {
            closestInteractable.OnEventTrigger.Invoke();
        }

    }



    private PlayerStateMachine _PlayerStateMachine;
}