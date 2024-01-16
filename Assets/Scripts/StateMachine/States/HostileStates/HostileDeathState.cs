using System.Collections;

using UnityEngine;

public class HostileDeathState : State {

    public HostileDeathState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _HostileStateMachine = GetStateMachine() as HostileStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
    }

    public override void EnterState() {
        GetStateMachine().GetActor().gameObject.tag = "Untagged"; // Prevent the player from hitting the dead entity
        GetStateMachine().GetActor().GetComponent<Collider2D>().enabled = false;

        GetStateMachine().GetActor().GetAnimator().Play(_HostileStateMachine._Colour + "Death");
        _HostileStateMachine.PlayRandomDeathSound();
        GetStateMachine().GetActor().StartCoroutine(WaitForDeathSound());
        GetStateMachine().GetActor().StartCoroutine(WaitForAnim());
    }

    public override void UpdateState() {
        if (_animationHasFinished && _deathSoundHasFinished) {
            Object.Destroy(GetStateMachine().GetActor().gameObject);
        }
    }

    IEnumerator WaitForAnim() {
        float animDuration = GetStateMachine().GetActor().GetAnimator().GetAnimationDuration();
        yield return new WaitForSeconds(animDuration - 0.25f);
        _animationHasFinished = true;
    }

    IEnumerator WaitForDeathSound() {
        yield return new WaitForSeconds(1f);
        _deathSoundHasFinished = true;
    }

    public override void ExitState() {

    }

    public override void AddSwitchCases() {

    }

    private HostileStateMachine _HostileStateMachine;

    private bool _animationHasFinished = false;
    private bool _deathSoundHasFinished = false;
}
