using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private DialogueManager2 diagmgr;
    [SerializeField] private WindowManager wm;

    public DialogueManager2 GetDialogueManager(){
        return diagmgr;
    }

    public WindowManager GetWindowManager(){
        return wm;
    }
}