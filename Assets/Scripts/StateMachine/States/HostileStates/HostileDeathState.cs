using System.Collections;

using UnityEngine;

public class HostileDeathState : State {
    private bool animationHasFinished = false;

    public HostileDeathState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _HostileStateMachine = GetStateMachine() as HostileStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
    }

    public override void EnterState() {
        GetStateMachine().GetActor().gameObject.tag = "Untagged";
        GetStateMachine().GetActor().GetAnimator().Play("Death");
        GetStateMachine().GetActor().StartCoroutine(WaitForAnim());

    }

    public override void UpdateState() {
        if (animationHasFinished) {
            Object.Destroy(GetStateMachine().GetActor().gameObject);
        }

    }

    IEnumerator WaitForAnim() {
        float animDuration = GetStateMachine().GetActor().GetAnimator().GetAnimationDuration();
        yield return new WaitForSeconds(animDuration + 0.25f);
        animationHasFinished = true;
    }

    public override void ExitState() {

    }

    public override void AddSwitchCases() {

    }

    private HostileStateMachine _HostileStateMachine;
}
