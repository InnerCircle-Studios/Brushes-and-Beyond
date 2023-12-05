using UnityEngine;
using UnityEngine.InputSystem;

class InputHandler : MonoBehaviour {
    public void OnMove(InputAction.CallbackContext context) {
        EventBus.TriggerEvent(EventBusEvents.EventName.MOVEMENT_KEYS, context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context) {
        EventBus.TriggerEvent(EventBusEvents.EventName.SPACE_KEY, context.ReadValueAsButton());
    }

    public void OnRun(InputAction.CallbackContext context) {
        EventBus.TriggerEvent(EventBusEvents.EventName.SHIFT_KEY, context.ReadValueAsButton());
    }

    public void OnDash(InputAction.CallbackContext context) {
        EventBus.TriggerEvent(EventBusEvents.EventName.G_KEY, context.ReadValueAsButton());
    }

    public void OnInteract(InputAction.CallbackContext context) {
        EventBus.TriggerEvent(EventBusEvents.EventName.E_KEY, context.ReadValueAsButton());
    }

    public void OnShow(InputAction.CallbackContext context) {
        EventBus.TriggerEvent(EventBusEvents.EventName.F_KEY, context.ReadValueAsButton());
    }

    public void OnPause(InputAction.CallbackContext context) {
        EventBus.TriggerEvent(EventBusEvents.EventName.PAUSE_KEY, context.ReadValueAsButton());
    }
}