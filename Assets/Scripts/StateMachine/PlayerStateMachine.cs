using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerStateMachine(Actor actor) : base(actor)
    {
        EventBus.StartListening<Vector2>(EventBusEvents.EventName.MOVEMENT_KEYS, OnMoveEvent);
        EventBus.StartListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttackEvent);
        EventBus.StartListening<bool>(EventBusEvents.EventName.DEATH_EVENT, OnDeathEvent);
        EventBus.StartListening<bool>(EventBusEvents.EventName.F_KEY, OnShowEvent);
        EventBus.StartListening<bool>(EventBusEvents.EventName.DIALOGUE_EVENT, OnDialogueEvent);
        EventBus.StartListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnRunEvent);

        AddState(new PlayerIdleState("PlayerIdleState", this));
        AddState(new PlayerAttackState("PlayerAttackState", this));
        AddState(new PlayerDashState("PlayerDashState", this));
        AddState(new PlayerDeathState("PlayerDeathState", this));
        AddState(new PlayerDialogueState("PlayerDialogueState", this));
        AddState(new PlayerRunState("PlayerRunState", this));
        AddState(new PlayerShowState("PlayerShowState", this));
        AddState(new PlayerWalkState("PlayerWalkState", this));

        ChangeState(GetState("PlayerIdleState"));

        InitSwitchCases();
    }

    ~PlayerStateMachine()
    {
        EventBus.StopListening<Vector2>(EventBusEvents.EventName.MOVEMENT_KEYS, OnMoveEvent);
        EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnAttackEvent);
        EventBus.StopListening<bool>(EventBusEvents.EventName.DEATH_EVENT, OnDeathEvent);
        EventBus.StopListening<bool>(EventBusEvents.EventName.F_KEY, OnShowEvent);
        EventBus.StopListening<bool>(EventBusEvents.EventName.DIALOGUE_EVENT, OnDialogueEvent);
        EventBus.StopListening<bool>(EventBusEvents.EventName.SHIFT_KEY, OnRunEvent);
    }

    public void OnMoveEvent(Vector2 movement)
    {
        _CurrentMovementInput = movement;

        _IsMovementPressed.Value = _CurrentMovementInput.x != 0 || _CurrentMovementInput.y != 0;
    }

    public void OnAttackEvent(bool isAttackPressed)
    {
        _IsAttackPressed.Value = isAttackPressed;
    }

    public void OnDeathEvent(bool isDeath)
    {
        _IsDeath.Value = isDeath;
    }

    public void OnShowEvent(bool isShowPressed)
    {
        _IsShowPressed.Value = isShowPressed;

        _IsShowDone.Value = false;
    }

    public void OnDialogueEvent(bool isDialogueActive)
    {
        _IsDialogueActive.Value = isDialogueActive;
    }

    public void OnRunEvent(bool isRunningPressed)
    {
        _IsRunningPressed.Value = isRunningPressed;
    }

    public Vector2 _CurrentMovementInput = new Vector2();
    public BoolWrapper _IsMovementPressed { get; set; } = new BoolWrapper(false);
    public BoolWrapper _IsDeath { get; set; } = new BoolWrapper(false);
    public BoolWrapper _IsAttackPressed { get; set; } = new BoolWrapper(false);
    public BoolWrapper _IsShowPressed { get; set; } = new BoolWrapper(false);
    public BoolWrapper _IsDialogueActive { get; set; } = new BoolWrapper(false);
    public BoolWrapper _IsShowDone { get; set; } = new BoolWrapper(false);
    public BoolWrapper _IsRunningPressed { get; set; } = new BoolWrapper(false);
    public BoolWrapper _AttackTimer {get; set;} = new BoolWrapper(false);
}