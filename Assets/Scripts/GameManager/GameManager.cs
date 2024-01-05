using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private WindowManager windowManager;
    [SerializeField] private Player player;

    public DialogueManager2 GetDialogueManager(){
        return dialogueManager;
    }

    public WindowManager GetWindowManager(){
        return windowManager;
    }

    public Player GetPlayer(){
        return player;
    }
}