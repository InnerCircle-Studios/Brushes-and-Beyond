using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public UnityEvent OnEventTrigger = new();
    [SerializeField] private DialogueSet dialogueSet;

    private SpriteRenderer activationKey;

    private void Start() {
        activationKey = gameObject.GetComponent<SpriteRenderer>();
    }

    //TODO add method to change dialogue when a quest is updated.


    public void ActivateIndicator() {
        activationKey.enabled = true;
        if(dialogueSet !=null){
            FindAnyObjectByType<GameManager>().GetDialogueManager().SetActiveDialogue(dialogueSet); // Load the dialogue set into the dialogue manager.1
        }
    }

    public void DeactivateIndicator() {
        activationKey.enabled = false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .3f);
    }
}