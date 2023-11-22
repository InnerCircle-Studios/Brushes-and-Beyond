using UnityEngine;

public class EventManager : MonoBehaviour {
    private EventManager() { }

    public void OnDeath(bool isDeath) 
    {
        EventBus.TriggerEvent(EventBusEvents.EventName.DEATH_EVENT, isDeath);
    }

    public void OnDialogue(bool isDialogueTrue) 
    {
        EventBus.TriggerEvent(EventBusEvents.EventName.DIALOGUE_EVENT, isDialogueTrue);
    }

    static public EventManager GetEventManager() {
        if (_eventManager == null) {
            _eventManager = FindObjectOfType<EventManager>();
        }

        return _eventManager;
    }

    private static EventManager _eventManager;
}