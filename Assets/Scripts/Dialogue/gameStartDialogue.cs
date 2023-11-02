using UnityEngine;

public class gameStartDialogue : MonoBehaviour {
    public DialogueTrigger trigger;
    public void gameStartDialogueTrigger() {
        trigger.StartDialogue();
    }
}