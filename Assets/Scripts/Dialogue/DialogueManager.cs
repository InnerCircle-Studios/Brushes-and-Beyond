using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    private Coroutine typeTextCoroutine;
     public Button nextButton;

    public static bool isActive = false;
    private bool _tutorial = true;


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
        nextButton.gameObject.SetActive(false);
        Debug.Log("OpenDialogue for these messages:" + messages.Length);
        backgroundBox.transform.localScale = new Vector3(11f, 2.5f, 1f);
        PlayActions();
        DisplayMessage();
    }

    void DisplayMessage() {
        Message messageToDisplay = currentMessages[currentMessageIndex];
        AudioManager.instance.PlaySfx("Dialogue");
        if (typeTextCoroutine != null) {
            StopCoroutine(typeTextCoroutine);
        }
        AudioManager.instance.StopSfx("Dialogue");
        typeTextCoroutine = StartCoroutine(TypeText(messageToDisplay.message));
        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    IEnumerator TypeText(string text) {
        messageText.text = ""; // Reset the text
        foreach (char letter in text.ToCharArray()) {
            messageText.text += letter;
            yield return new WaitForSeconds(0.05f); // Delay between characters. Adjust as needed.
        }
        nextButton.gameObject.SetActive(true);
    }

    void PlayActions() {
        // Amazing linq query to find the actions to play after the current message index. 
        currentActions.ToList().Where(a => a.PlayAfterIndex == currentMessageIndex - 1).ToList().ForEach(e => e.Action.Invoke());
        
    }


    public void NextMessage() {
        currentMessageIndex++;
        PlayActions();
        if (currentMessageIndex < currentMessages.Length) {
            nextButton.gameObject.SetActive(false);
            DisplayMessage();

        }
        else {
            Debug.Log("No more messages");
            isActive = false;
            backgroundBox.transform.localScale = Vector3.zero;
            nextButton.gameObject.SetActive(false);
            if (_tutorial)
            {
                PlayerStateMachine ctx = FindAnyObjectByType<PlayerStateMachine>();
                _tutorial = false;
            }
        }
    }

    void Start() {
        backgroundBox.transform.localScale = Vector3.zero;
    }





}
