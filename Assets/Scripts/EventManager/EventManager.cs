using UnityEngine;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour {
    private EventManager() { }

    public void OnMove(InputAction.CallbackContext context) {
        OnMoveEvent(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context) {
        OnAttackEvent(context.ReadValueAsButton());
    }

    public void OnRun(InputAction.CallbackContext context) {
        OnRunEvent(context.ReadValueAsButton());
    }

    public void OnDash(InputAction.CallbackContext context) {
        OnDashEvent();
    }

    public void OnInteract(InputAction.CallbackContext context) {
        OnInteractEvent(context.ReadValueAsButton());
    }

    public void OnShow(InputAction.CallbackContext context) {
        OnShowEvent(context.ReadValueAsButton());
    }

    public void OnDeath(bool isDeath) {
        OnDeathEvent(isDeath);
    }

    public void OnDialogue(bool isDialogueTrue) {
        OnDialogueEvent(isDialogueTrue);
    }

    static public EventManager GetEventManager() {
        if (_eventManager == null) {
            _eventManager = FindObjectOfType<EventManager>();
        }

        return _eventManager;
    }

    public delegate void Action();
    public delegate void Bool(bool isDeath);
    public delegate void MoveEvent(Vector2 move);
    public event MoveEvent OnMoveEvent;

    public event Bool OnAttackEvent;

    public event Bool OnRunEvent;

    public event Action OnDashEvent;

    public event Bool OnInteractEvent;

    public event Bool OnShowEvent;

    public event Bool OnDeathEvent;

    public event Bool OnDialogueEvent;

    private static EventManager _eventManager;
}