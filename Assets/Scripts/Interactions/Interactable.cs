using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public UnityEvent OnEventTrigger = new();
    [SerializeField] private DialogueSet dialogueSet;

    private DialogueSet questDialogueSet;
    [SerializeField, Range(0, 10)] private float interactionRange;

    private SpriteRenderer activationKey;
    private GameManager gameManager;

    private void OnEnable() {
        QuestEvents.OnChangeDialogue += ChangeDialogue;
        QuestEvents.OnOverrideBaseDialogue += OverrideBaseDialogue;
        activationKey = gameObject.GetComponent<SpriteRenderer>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnDisable() {
        QuestEvents.OnChangeDialogue -= ChangeDialogue;
        QuestEvents.OnOverrideBaseDialogue -= OverrideBaseDialogue;
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
    }

    public void DeactivateIndicator() {
        activationKey.enabled = false;
    }

    public float GetInteractionRange() {
        return interactionRange;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .3f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}