using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueAction {
    [SerializeField] public int PlayAfterIndex;
    [SerializeField] public EventWrapper Action;
    [SerializeField,HideInInspector] private bool hasBeenTriggered = false;
    [SerializeField] public bool OneTimeEvent = false;

    public DialogueAction(int playAfterIndex, EventWrapper action, bool oneTimeEvent) {
        PlayAfterIndex = playAfterIndex;
        Action = action;
        OneTimeEvent = oneTimeEvent;
    }

    public bool HasBeenTriggered() {
        return hasBeenTriggered;
    }
    public void Trigger() {
        Action.Invoke();
        hasBeenTriggered = true;
    }

    public void Reset() {
        hasBeenTriggered = false;
    }

}

public enum DialogueActionType{
    ACTIVATE_OBJECT,
    DEACTIVATE_OBJECT,
    PLAY_ANIMATION,
    CAMERA_MOVE
}

[Serializable]
public class EventWrapper : UnityEvent { }