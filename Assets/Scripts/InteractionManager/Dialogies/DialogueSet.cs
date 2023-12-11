using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSet", menuName = "Brushes/Dialogue/DialogueSet")]
public class DialogueSet {
    [SerializeField] private List<DialogueEntry> DialogueList = new();
    [SerializeField] private int currentIndex = 0;

    public DialogueEntry GetNextEntry() {
        currentIndex++;
        if (currentIndex >= DialogueList.Count) {
            return new();
        }
        return DialogueList[currentIndex];
    }

    public DialogueEntry GetCurrentEntry() {
        if (currentIndex >= DialogueList.Count) {
            return new();
        }
        return DialogueList[currentIndex];
    }

}