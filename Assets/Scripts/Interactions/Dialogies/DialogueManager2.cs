using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class DialogueManager2 : MonoBehaviour {
    [SerializeField] private DialogueSet activeSet;
    [SerializeField] private List<DialogueSet> dialogueSets = new();
    private WindowManager wm;

    private void Start() {
        wm = FindAnyObjectByType<WindowManager>();
    }

    public void SetActiveDialogue(DialogueSet set) {
        activeSet = set;
    }
    public void SetActiveDialogueSet(int index) {
        activeSet = dialogueSets[index];
    }


    public void StartDialogueSet() {
        EventBus.TriggerEvent(EventBusEvents.EventName.DIALOGUE_EVENT, true);
        wm.InitDialogueBox(activeSet.GetCurrentEntry());
    }

    // loads the next line to the dialogue box. Sends an exit event if there are no more lines.
    public void NextEntry() {
        DialogueEntry nextEntry = activeSet.GetNextEntry();
        if (nextEntry != null) {
            wm.InitDialogueBox(nextEntry);
        }
        else {
            EventBus.TriggerEvent(EventBusEvents.EventName.DIALOGUE_EVENT, false);
            activeSet.ResetIndex();
            wm.HideDialogueBox();
        }
    }

}