using System.Collections;

using UnityEngine;

public class PaintTubeDeathState : State {
    private bool animationHasFinished = false;
    public PaintTubeDeathState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PaintStateMachine = GetStateMachine() as PaintTubeStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
    }

    public override void EnterState() {
        GetStateMachine().GetActor().gameObject.tag = "Untagged"; // Prevent the player from hitting the dead entity
        GetStateMachine().GetActor().GetComponent<Collider2D>().enabled = false;
        _PaintStateMachine.PlayRandomDeathSound();
        GetStateMachine().GetActor().GetAnimator().Play(_PaintStateMachine._colour + "Death");
        GetStateMachine().GetActor().StartCoroutine(WaitForAnim());
    }

    public override void UpdateState() {
        if (animationHasFinished) {
            Actor me = GetStateMachine().GetActor();
            Object.Instantiate(me.GetAttrubuteManager().GetAttributes().Loot, me.GetRigidBody().position, Quaternion.identity);
            Object.Destroy(GetStateMachine().GetActor().gameObject);
        }

    }

    public override void ExitState() {

    }

    public override void AddSwitchCases() {

    }

    IEnumerator WaitForAnim() {
        float animDuration = GetStateMachine().GetActor().GetAnimator().GetAnimationDuration();
        yield return new WaitForSeconds(animDuration);
        animationHasFinished = true;
    }

    private PaintTubeStateMachine _PaintStateMachine;
}