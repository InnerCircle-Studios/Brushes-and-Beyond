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
    public void Db(){
        Debug.Log("Die, die, dynamite! Halleluja!");
    }
    public void Db2(){
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSecondsRealtime(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
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