using UnityEngine;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{

    private EventManager() { }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent(context.ReadValue<Vector2>());
    }

    static public EventManager GetEventManager()
    {
        if (_eventManager == null)
        {
            _eventManager = FindObjectOfType<EventManager>();
        }
        
        return _eventManager;
    }
 
    public delegate void MoveEvent(Vector2 move);
    public event MoveEvent OnMoveEvent;

    private static EventManager _eventManager;
}