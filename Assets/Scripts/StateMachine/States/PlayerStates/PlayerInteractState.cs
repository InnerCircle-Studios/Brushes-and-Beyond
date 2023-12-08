using System.Linq;

using UnityEngine;

public class PlayerInteractState : State {
    private float interactionRange;

    public PlayerInteractState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
       interactionRange =  _PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes().InteractionRange;
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
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._IsDialogueActive, false), _PlayerStateMachine.GetState("PlayerIdleState"));
    }

    private void CheckInteractions() {
        // Get current location and every collider within a set range
        Vector2 currentPosition = _PlayerStateMachine.GetActor().transform.position;


        Interactable closestInteractable = null;
        float smallestDistance = 100f;

        // Get the closest interactable and invoke it
        foreach (Interactable interactable in Object.FindObjectsOfType<Interactable>()) {
            Debug.Log(interactable.transform.gameObject.name);

            float distanceBetweenTargets = Vector2.Distance(currentPosition, interactable.gameObject.transform.position);
            Debug.Log(distanceBetweenTargets + "  -  "+interactionRange);
            if (distanceBetweenTargets < smallestDistance && distanceBetweenTargets <= interactionRange) {
                closestInteractable = interactable;
                smallestDistance = distanceBetweenTargets;
            }

        }

        Debug.Log(closestInteractable);
        if (closestInteractable != null) {
            closestInteractable.OnEventTrigger?.Invoke();
        }
    }



    private PlayerStateMachine _PlayerStateMachine;
}