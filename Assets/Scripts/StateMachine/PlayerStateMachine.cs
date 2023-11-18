using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerStateMachine(Actor actor) : base(actor)
    {
        GetEventManager().OnMoveEvent += OnMoveEvent;
        GetEventManager().OnAttackEvent += OnAttackEvent;
        GetEventManager().OnDeathEvent += OnDeathEvent;
        GetEventManager().OnShowEvent += OnShowEvent;
        GetEventManager().OnDialogueEvent += OnDialogueEvent;
        GetEventManager().OnRunEvent += OnRunEvent;

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

    ~PlayerStateMachine()
    {
        GetEventManager().OnMoveEvent -= OnMoveEvent;
        GetEventManager().OnAttackEvent -= OnAttackEvent;
        GetEventManager().OnDeathEvent -= OnDeathEvent;
        GetEventManager().OnShowEvent -= OnShowEvent;
        GetEventManager().OnDialogueEvent -= OnDialogueEvent;
        GetEventManager().OnRunEvent -= OnRunEvent;
    }

    public void OnMoveEvent(Vector2 movement)
    {
        _CurrentMovementInput = movement;

        _IsMovementPressed = _CurrentMovementInput.x != 0 || _CurrentMovementInput.y != 0;
    }

    public void OnAttackEvent(bool isAttackPressed)
    {
        _IsAttackPressed = isAttackPressed;
    }

    public void OnDeathEvent(bool isDeath)
    {
        _IsDeath = isDeath;
    }

    public void OnShowEvent(bool isShowPressed)
    {
        _IsShowPressed = isShowPressed;

        _IsShowDone = false;
    }

    public void OnDialogueEvent(bool isDialogueActive)
    {
        _IsDialogueActive = isDialogueActive;
    }

    public void OnRunEvent(bool isRunningPressed)
    {
        _IsRunningPressed = isRunningPressed;
    }

    public Vector2 _CurrentMovementInput = new Vector2();
    public bool _IsMovementPressed = false;
    public bool _IsDeath = false;
    public bool _IsAttackPressed = false;
    public bool _IsShowPressed = false;
    public bool _IsDialogueActive = false;
    public bool _IsShowDone = false;
    public bool _IsRunningPressed = false;
}