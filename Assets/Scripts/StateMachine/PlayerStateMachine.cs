using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerStateMachine(Actor actor) : base(actor)
    {
        GetEventManager().OnMoveEvent += OnMoveEvent;
        GetEventManager().OnDeathEvent += OnDeathEvent;

        AddState(new PlayerIdleState("PlayerIdleState", this));
        AddState(new PlayerAttackState("PlayerAttackState", this));
        AddState(new PlayerDashState("PlayerDashState", this));
        AddState(new PlayerDeathState("PlayerDeathState", this));
        AddState(new PlayerDialogueState("PlayerDialogueState", this));
        AddState(new PlayerRunState("PlayerRunState", this));
        AddState(new PlayerShowState("PlayerShowState", this));
        AddState(new PlayerWalkState("PlayerWalkState", this));

        ChangeState(GetState("PlayerIdleState"));
    }

    public void OnMoveEvent(Vector2 movement)
    {
        _CurrentMovementInput = movement;

        _isMovementPressed = _CurrentMovementInput.x != 0 || _CurrentMovementInput.y != 0;
    }

    public void OnDeathEvent(bool isDeath)
    {
        _IsDeath = isDeath;
    }

    public Vector2 _CurrentMovementInput;
    public bool _isMovementPressed;
    public bool _IsDeath;
}