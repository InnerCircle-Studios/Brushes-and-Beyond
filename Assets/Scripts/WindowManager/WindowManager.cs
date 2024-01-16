using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;

public class WindowManager : MonoBehaviour {
    [SerializeField] private List<Window> staticWindows;
    [SerializeField] private List<TextWindow> textWindows;

    [SerializeField] private DialogueBox dialogueBox;
    [SerializeField] private QuestBox questBox;
    [SerializeField] private HealthBarWindow healthBarWindow;
    // insert custom classes for tutorial etc (or make them static)

    private void Awake() {
        EventBus.StartListening<bool>(EventBusEvents.EventName.PAUSE_KEY, (bool keyPressed) => ToggleWindow("PauseMenu"));
    }

    // Dialogue
    public DialogueBox GetDialogueBox() {
        return dialogueBox;
    }
    public void InitDialogueBox(DialogueBox box) {
        ShowDialogueBox();
        dialogueBox.LoadBox(box);
    }
    public void InitDialogueBox(Actor actor) {
        ShowDialogueBox();
        dialogueBox.LoadActor(actor);
    }
    public void InitDialogueBox(CharacterData actor) {
        ShowDialogueBox();
        dialogueBox.LoadActor(actor);
    }
    public void InitDialogueBox(DialogueEntry diagEntry) {
        ShowDialogueBox();
        dialogueBox.LoadActor(diagEntry.Actor);
        UpdateDialogueBox(diagEntry.Dialogue);
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

    // QuestMenu
    public void SetQuestName(string name) {
        questBox.SetName(name);
    }
    public void SetQuestObjectives(string text) {
        questBox.SetObjectives(text);
    }
    public void AddQuestObjective(string text) {
        questBox.AddObjective(text);
    }
    public void ClearQuest() {
        questBox.SetName("");
        questBox.SetObjectives("");
    }
    public void ShowQuestMenu() {
        questBox.gameObject.SetActive(true);
    }
    public void HideQuestMenu() {
        questBox.gameObject.SetActive(false);
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


    //Healthbar
    public void ShowHealthBar() {
        healthBarWindow.enabled = false;
    }
    public void HideHealthBar() {
        healthBarWindow.enabled = false;
    }
    public void UpdateHealthBar(int currentHP) {
        healthBarWindow.SetSprite(currentHP);
    }


    // Global
    public void ClearScreen(string[] excludedScreens) {
        staticWindows?.FindAll(e => !excludedScreens.Contains(e.Name)).ForEach(e => HideWindow(e.Name));
        textWindows?.FindAll(e => !excludedScreens.Contains(e.Name)).ForEach(e => HideWindow(e.Name));
    }
}
