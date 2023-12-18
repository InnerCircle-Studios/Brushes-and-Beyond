using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;

public class WindowManager : MonoBehaviour {
    [SerializeField] private List<Window> staticWindows;
    [SerializeField] private List<TextWindow> textWindows;

    [SerializeField] private DialogueBox dialogueBox;
    // insert custom classes for tutorial etc (or make them static)



    // Dialogue
    public DialogueBox GetDialogueBox() {
        return dialogueBox;
    }
    public void InitDialogueBox(DialogueBox box){
        ShowDialogueBox();
        dialogueBox.LoadBox(box);
    }
    public void InitDialogueBox(Actor actor) {
        ShowDialogueBox();
        dialogueBox.LoadCharacter(actor);
    }
    public void UpdateDialogueBox(string text) {
        dialogueBox.SetMessage(text);
    }
    public void ShowDialogueBox() {
        dialogueBox.gameObject.SetActive(true);
    }
    public void HideDialogueBox() {
        dialogueBox.gameObject.SetActive(false);
    }


    // static screens
    public void ShowWindow(string window) {
        staticWindows?.First(e => e.Name == window).Element.SetActive(true);
    }
    public void HideWindow(string window) {
        staticWindows?.First(e => e.Name == window).Element.SetActive(false);
    }
    public void ToggleWindow(string window) {
        GameObject screen = staticWindows?.First(e => e.Name == window).Element;
        screen.SetActive(!screen.activeSelf);
    }

    // Text windows
    public void ShowTextWindow(string window) {
        TextMeshProUGUI text = textWindows?.First(e => e.Name == window).Element;
        text.enabled = true;
    }
    public void HideTextWindow(string window) {
        TextMeshProUGUI text = textWindows?.First(e => e.Name == window).Element;
        text.enabled = false;
    }
    public void UpdateTextWindow(string window, string content) {
        TextWindow screen = textWindows?.First(e => e.Name == window);
        screen.Element.SetText(screen.DefaultText + content);
    }


    // Global
    public void ClearScreen(string[] excludedScreens) {
        staticWindows?.FindAll(e => !excludedScreens.Contains(e.Name)).ForEach(e => HideWindow(e.Name));
        textWindows?.FindAll(e => !excludedScreens.Contains(e.Name)).ForEach(e => HideWindow(e.Name));
    }
}
