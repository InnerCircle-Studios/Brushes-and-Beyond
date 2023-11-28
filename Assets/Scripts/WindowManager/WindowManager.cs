using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class WindowManager : MonoBehaviour {
    [SerializeField] private List<Window> staticWindows;
    [SerializeField] private DialogueBox dialogueBox;
    // insert custom classes for tutorial etc (or make them static)



    // Dialogue
    public DialogueBox GetDialogueBox() {
        return dialogueBox;
    }
    public void InitDialogueBox(Actor actor){
        ShowDialogueBox();
        dialogueBox.LoadCharacter(actor);
    }
    public void ShowDialogueBox() {
        dialogueBox.enabled = true;
    }
    public void HideDialogueBox() {
        dialogueBox.enabled = false;
    }

    // static screens
    public void ShowScreen(string screen) {
        staticWindows?.First(e => e.Name == screen).Element.SetActive(true);
    }
    public void HideScreen(string screen) {
        staticWindows?.First(e => e.Name == screen).Element.SetActive(false);
    }
    public void ToggleScreen(string screen) {
        GameObject window = staticWindows?.First(e => e.Name == screen).Element;
        window.SetActive(!window.activeSelf);

    }

    public void ClearScreen(string[] excludedScreens) {
        staticWindows?.FindAll(e => !excludedScreens.Contains(e.Name)).ForEach(e => HideScreen(e.Name));
    }
}
