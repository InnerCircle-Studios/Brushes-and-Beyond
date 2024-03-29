using System.Collections;

using UnityEngine;

public class PlayerAttackState : State {
    public PlayerAttackState(string name, StateMachine stateMachine) : base(name, stateMachine) {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void AwakeState() {
        AddSwitchCases();
    }

    public override void EnterState() {
        _PlayerStateMachine.PlayAttackSound();
        Actor actor = GetStateMachine().GetActor();
        actor.GetAnimator().Play("SwordSwing", _PlayerStateMachine._CurrentDirection);
        Dash();
        actor.HandleMeleeAttack();
        actor.StartCoroutine(WaitForAttack());
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() {
        _PlayerStateMachine._AttackTimer.Value = false;
    }

    public override void AddSwitchCases() {
        AddSwitchCase(new SwitchCaseWrapper(_PlayerStateMachine._AttackTimer, true), _PlayerStateMachine.GetState("PlayerIdleState"));
    }

    private IEnumerator WaitForAttack() {
        yield return new WaitForSeconds(0.5f);

        _PlayerStateMachine._AttackTimer.Value = true;
    }

    private void Dash() {
        _PlayerStateMachine.GetActor().Dash(_PlayerStateMachine._CurrentMovementInput);
    }

    private PlayerStateMachine _PlayerStateMachine;
}
