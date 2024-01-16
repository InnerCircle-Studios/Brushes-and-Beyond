using UnityEngine;

public class PlayerDeathState : State {
    public PlayerDeathState(string name, StateMachine stateMachine) : base(name, stateMachine) {

    }

    public override void AwakeState() {
        AddSwitchCases();
    }

    public override void EnterState() {
        GameManager.Instance.GetWindowManager().ClearQuest();
        GetStateMachine().GetActor().GetComponent<Collider2D>().enabled = false;
    }

    public override void ExitState() {

    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void AddSwitchCases() {

    }
}
