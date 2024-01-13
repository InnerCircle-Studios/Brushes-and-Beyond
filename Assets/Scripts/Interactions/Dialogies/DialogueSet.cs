using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class DialogueSet {

    [Header("Dialogue")]
    [SerializeField] private List<DialogueEntry> dialogueList = new();
    [SerializeField] private List<DialogueAction> actionList = new();
    [SerializeField, HideInInspector] private int currentIndex = 0;

    public DialogueSet(List<DialogueEntry> dialogueList, List<DialogueAction> actionList) {
        this.dialogueList = dialogueList;
        this.actionList = actionList;
    }

    public DialogueEntry GetNextEntry() {
        actionList.Where(action => action.PlayAfterIndex == currentIndex && !action.HasBeenTriggered()).ToList()
                  .ForEach(action => action.Trigger());

        currentIndex++;
        if (currentIndex > dialogueList.Count - 1) {
            actionList.Where(action => action.PlayAfterIndex > currentIndex && !action.HasBeenTriggered()).ToList()
                  .ForEach(action => action.Trigger()); // Trigger all actions marked to be played after the dialogue has finished.
            return null;
        }
        return dialogueList[currentIndex];
    }

    public DialogueEntry GetCurrentEntry() {
        actionList.Where(action => action.PlayAfterIndex == currentIndex && !action.HasBeenTriggered()).ToList()
                  .ForEach(action => action.Trigger());

        if (currentIndex > dialogueList.Count - 1) {
            return null;
        }
        return dialogueList[currentIndex];
    }

    public void ResetIndex() {
        currentIndex = 0;
        actionList.Where(action => !action.OneTimeEvent).ToList().ForEach(action => action.Reset());
    }

    public int GetCurrentIndex() {
        return currentIndex;
    }

}