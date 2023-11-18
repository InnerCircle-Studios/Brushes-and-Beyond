using UnityEngine;

public class PlayerWalkState : State
{
    public PlayerWalkState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
        _PlayerStateMachine = GetStateMachine() as PlayerStateMachine;
    }

    public override void AwakeState()
    {
        AddSwitchCases();
    }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        HandleWalk();
    }

    public override void AddSwitchCases() 
    {
        AddSwitchCase(_PlayerStateMachine._IsDeath, _PlayerStateMachine.GetState("PlayerDeathState"));
        AddSwitchCase(!_PlayerStateMachine._IsMovementPressed, _PlayerStateMachine.GetState("PlayerIdleState"));
        AddSwitchCase(_PlayerStateMachine._IsDialogueActive, _PlayerStateMachine.GetState("PlayerDialogueState"));
        AddSwitchCase(_PlayerStateMachine._IsAttackPressed, _PlayerStateMachine.GetState("PlayerAttackState"));
    }

    private void HandleWalk()
    {
        float currentSpeed = _PlayerStateMachine.GetActor().GetAttrubuteManager().GetAttributes().Speed;
        Vector2 desiresMovement = new Vector2(_PlayerStateMachine._CurrentMovementInput.x, _PlayerStateMachine._CurrentMovementInput.y).normalized * currentSpeed * Time.deltaTime;
        _PlayerStateMachine.GetActor().HandleWalk(desiresMovement);
    }

    private PlayerStateMachine _PlayerStateMachine;
}
