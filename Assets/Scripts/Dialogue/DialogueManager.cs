using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{

    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;

    private Message[] currentMessages;
    private Actor[] currentActors;
    private int currentMessageIndex = 0;
    private bool _isInteractPressed = false;

    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        currentMessageIndex = 0;
        isActive = true;
        Debug.Log("OpenDialoage for these messages:" + messages.Length);
        backgroundBox.transform.localScale = new Vector3(11f, 2.5f, 1f);
        DisplayMessage();
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[currentMessageIndex];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextMessage()
    {
        currentMessageIndex++;
        if (currentMessageIndex < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("No more messages");
            isActive = false;
            backgroundBox.transform.localScale = Vector3.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    
   /* public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _isInteractPressed = context.ReadValueAsButton();
            if (_isInteractPressed && isActive)
            {
                NextMessage();
                Debug.Log("Going to next message");
            }
        }
    }*/


}
