using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour {
    public Message[] messages;
    public Actor[] actors;
    public DialogueAction[] DialogueActions;
    public void StartDialogue() {
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors, DialogueActions);
    }

}


[System.Serializable]
public class Message {
    public int actorId;
    public string message;
}
[System.Serializable]
public class Actor {
    public string name;
    public Sprite sprite;

}
[System.Serializable]
public class DialogueAction {
    public int PlayAfterIndex;
    public UnityEvent Action;
}