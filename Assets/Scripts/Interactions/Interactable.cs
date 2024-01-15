using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, ISaveable {

    [Header("General")]
    public UnityEvent OnEventTrigger = new();
    [SerializeField, Range(0, 10)] private float interactionRange;
    [SerializeField] private bool autoTrigger = false;

    [Header("Dialogue")]
    [SerializeField] private DialogueSet dialogueSet;


    private DialogueSet questDialogueSet;
    private DialogueSet savedDialogueSet;
    private SpriteRenderer activationKey;
    private GameManager gameManager;
    private bool hasBeenTriggered;


    private void OnEnable() {
        QuestEvents.OnChangeDialogue += ChangeDialogue;
        QuestEvents.OnOverrideBaseDialogue += OverrideBaseDialogue;
        activationKey = gameObject.GetComponentInChildren<SpriteRenderer>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnDisable() {
        QuestEvents.OnChangeDialogue -= ChangeDialogue;
        QuestEvents.OnOverrideBaseDialogue -= OverrideBaseDialogue;
    }

    private void Start() {
        if (savedDialogueSet != null) {
            dialogueSet = savedDialogueSet;
        }
    }

    private void ChangeDialogue(Dictionary<string, DialogueSet> data) {
        if (data.ContainsKey(gameObject.name)) {
            questDialogueSet = data[gameObject.name];
        }
        else if (transform.parent != null && data.ContainsKey(transform.parent.gameObject.name)) {
            questDialogueSet = data[transform.parent.gameObject.name];
        }
    }

    private void OverrideBaseDialogue(Dictionary<string, DialogueSet> data) {
        if (data.ContainsKey(gameObject.name)) {
            dialogueSet = data[gameObject.name];
        }
        else if (transform.parent != null && data.ContainsKey(transform.parent.gameObject.name)) {
            dialogueSet = data[transform.parent.gameObject.name];
        }
    }

    public void ActivateIndicator() {
        activationKey.enabled = true;
        if (questDialogueSet != null) {
            gameManager.GetDialogueManager().SetActiveDialogue(questDialogueSet);
        }
        else if (dialogueSet != null) {
            FindAnyObjectByType<GameManager>().GetDialogueManager().SetActiveDialogue(dialogueSet); // Load the dialogue set into the dialogue manager.
        }
        if (autoTrigger && !hasBeenTriggered) {
            hasBeenTriggered = true;
            OnEventTrigger.Invoke();
        }
    }

    public void DeactivateIndicator() {
        activationKey.enabled = false;
    }

    public float GetInteractionRange() {
        return interactionRange;
    }

    public void SetAutoTrigger(bool value){
        autoTrigger = value;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .3f);
        if (autoTrigger) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, interactionRange);
        }
        else {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, interactionRange);
        }

    }

    public void LoadData(GameData data) {
        if (data.ObjectData.InteractionData.ContainsKey(gameObject.name)) {
            SerializableDict<string, DialogueSet> newData = data.ObjectData.InteractionData[gameObject.name];
            // newData.TryGetValue("QuestDialogueSet", out questDialogueSet);
            newData.TryGetValue("DialogueSet", out dialogueSet);
        }

    }

    public void SaveData(GameData data) {
        if (gameObject != null && this != null) {
            data.ObjectData.InteractionData[gameObject.name] = new(){
            // { "QuestDialogueSet", questDialogueSet },
            { "DialogueSet", dialogueSet }};
        }
    }
}