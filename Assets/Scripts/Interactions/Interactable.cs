using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public UnityEvent OnEventTrigger = new();
    [SerializeField] private DialogueSet dialogueSet;
    [SerializeField,Range(0,10)] private float interactionRange;

    private SpriteRenderer activationKey;

    private void Start() {
        activationKey = gameObject.GetComponent<SpriteRenderer>();
    }

    //TODO add method to change dialogue when a quest is updated.


    public void ActivateIndicator() {
        activationKey.enabled = true;
        if(dialogueSet !=null){
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