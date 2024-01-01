using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private DialogueManager2 diagmgr;
    [SerializeField] private WindowManager wm;
    [SerializeField] private Player player;

    public DialogueManager2 GetDialogueManager(){
        return diagmgr;
    }

    public WindowManager GetWindowManager(){
        return wm;
    }

    public Player GetPlayer(){
        return player;
    }
}