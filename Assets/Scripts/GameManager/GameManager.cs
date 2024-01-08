using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private WindowManager windowManager;
    [SerializeField] private Player player;
    [SerializeField] private Actor brushy;

    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Update(){
        if (player.GetAttrubuteManager().IsAlive() == false){
            windowManager.ShowWindow("GameOverMenu");
        }
    }
    
    public DialogueManager2 GetDialogueManager(){
        return dialogueManager;
    }

    public WindowManager GetWindowManager(){
        return windowManager;
    }

    public Player GetPlayer(){
        return player;
    }

    public Actor GetBrushy(){
        return brushy;
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void EndGame(){
        Application.Quit();
    }
}