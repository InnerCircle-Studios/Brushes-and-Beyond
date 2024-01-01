using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class DialogueSet {
    [SerializeField] private List<DialogueEntry> dialogueList = new();
    [SerializeField] private int currentIndex = 0;

    public DialogueSet(List<DialogueEntry> dialogueEntries){
        dialogueList = dialogueEntries;
    }

    public DialogueEntry GetNextEntry() {
        currentIndex++;
        if (currentIndex > dialogueList.Count-1) {
            return null;
        }
        return dialogueList[currentIndex];
    }

    public DialogueEntry GetCurrentEntry() {
        if (currentIndex > dialogueList.Count-1) {
            return null;
        }
        return dialogueList[currentIndex];
    }

    public void ResetIndex(){
        currentIndex = 0;
    }

    public int GetCurrentIndex(){
        return currentIndex;
    }

}