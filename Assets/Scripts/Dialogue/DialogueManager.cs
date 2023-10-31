using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;

public class DialogueManager : MonoBehaviour {

    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;

    private Message[] currentMessages;
    private Actor[] currentActors;
    private DialogueAction[] currentActions;
    private int currentMessageIndex = 0;

    public static bool isActive = false;
    private bool isEventExecuting = false;


    private static DialogueManager _instance;
    public static DialogueManager Instance {
        get { return _instance; }
    }


    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void OpenDialogue(Message[] messages, Actor[] actors, DialogueAction[] dialogueActions) {
        currentMessages = messages;
        currentActors = actors;
        currentActions = dialogueActions;
        currentMessageIndex = 0;
        isActive = true;
        Debug.Log("OpenDialogue for these messages:" + messages.Length);
        backgroundBox.transform.localScale = new Vector3(11f, 2.5f, 1f);
        PlayActions();
        DisplayMessage();
    }

    void DisplayMessage() {
        Message messageToDisplay = currentMessages[currentMessageIndex];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    void PlayActions() {
        // Amazing linq query to find the actions to play after the current message index. 
        currentActions.ToList().Where(a => a.PlayAfterIndex == currentMessageIndex - 1).ToList().ForEach(e => e.Action.Invoke());
        
    }


    public void NextMessage() {
        currentMessageIndex++;
        PlayActions();
        if (currentMessageIndex < currentMessages.Length) {
            DisplayMessage();

        }
        else {
            Debug.Log("No more messages");
            isActive = false;
            backgroundBox.transform.localScale = Vector3.zero;
        }
    }

    void Start() {
        backgroundBox.transform.localScale = Vector3.zero;
    }





}
