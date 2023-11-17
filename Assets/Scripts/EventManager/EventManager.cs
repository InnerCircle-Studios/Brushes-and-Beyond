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
        OnAttackEvent();
    }

    public void OnRun(InputAction.CallbackContext contect)
    {
        OnRunEvent();
    }

    public void OnDash(InputAction.CallbackContext contect)
    {
        OnDashEvent();
    }

    public void OnInteract(InputAction.CallbackContext contect)
    {
        OnInteractEvent();
    }

    public void OnShow(InputAction.CallbackContext contect)
    {
        OnShowEvent();
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
    public delegate void MoveEvent(Vector2 move);
    public event MoveEvent OnMoveEvent;

    public event Action OnAttackEvent;

    public event Action OnRunEvent;

    public event Action OnDashEvent;

    public event Action OnInteractEvent;

    public event Action OnShowEvent;

    private static EventManager _eventManager;
}