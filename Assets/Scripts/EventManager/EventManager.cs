using UnityEditor.Rendering.LookDev;

using UnityEngine;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{

    private EventManager() { }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        OnAttackEvent(context.ReadValueAsButton());
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        OnRunEvent();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        OnDashEvent();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        OnInteractEvent();
    }

    public void OnShow(InputAction.CallbackContext context)
    {
        OnShowEvent(context.ReadValueAsButton());
    }

    public void OnDeath(bool isDeath)
    {
        OnDeathEvent(isDeath);
    }

    static public EventManager GetEventManager()
    {
        if (_eventManager == null)
        {
            _eventManager = FindObjectOfType<EventManager>();
        }
        
        return _eventManager;
    }
 
    public delegate void Action();
    public delegate void Bool(bool isDeath);
    public delegate void MoveEvent(Vector2 move);
    public event MoveEvent OnMoveEvent;

    public event Bool OnAttackEvent;

    public event Action OnRunEvent;

    public event Action OnDashEvent;

    public event Action OnInteractEvent;

    public event Bool OnShowEvent;

    public event Bool OnDeathEvent;

    private static EventManager _eventManager;
}