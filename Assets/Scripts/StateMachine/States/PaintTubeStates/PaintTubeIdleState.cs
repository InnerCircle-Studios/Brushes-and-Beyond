using System.Collections;

using UnityEngine;

public class PaintTubeIdleState : State {
    public PaintTubeIdleState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PaintStateMachine = GetStateMachine() as PaintTubeStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
    }

    public override void EnterState() {
        isSittingDown = true;
        GetStateMachine().GetActor().StartCoroutine(WaitForSitDown());
    }

    public override void UpdateState() {
        if (!isSittingDown) {
            GetStateMachine().GetActor().GetAnimator().Play(_PaintStateMachine._colour + "Idle");
        }

        _PaintStateMachine.CheckPlayerInRange();
        CheckSwitchStates();
    }

    public override void ExitState() {
        // GetStateMachine().GetActor().StartCoroutine(WaitForStandup());
    }

    public override void AddSwitchCases() {
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isDead, true), _PaintStateMachine.GetState("PaintTubeDeathState"));
        AddSwitchCase(new SwitchCaseWrapper(_PaintStateMachine._isInRange, true), _PaintStateMachine.GetState("PaintTubeWalkState"));
    }

    private IEnumerator WaitForSitDown() {
        float animDuration = GetStateMachine().GetActor().GetAnimator().GetAnimationDuration();
        GetStateMachine().GetActor().GetAnimator().Play(_PaintStateMachine._colour + "SitDown");
        yield return new WaitForSeconds(0.66f);
        isSittingDown = false;
    }

    private IEnumerator WaitForStandup() {
        float animDuration = GetStateMachine().GetActor().GetAnimator().GetAnimationDuration();
        GetStateMachine().GetActor().GetAnimator().Play(_PaintStateMachine._colour + "StandUp");
        yield return new WaitForSeconds(animDuration + 0.20f);
    }

    private PaintTubeStateMachine _PaintStateMachine;
    private bool isSittingDown = true;
}