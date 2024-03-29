using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [Header("References")]
    [SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private WindowManager windowManager;
    [SerializeField] private Player player;

    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
    }

    private void Update() {
        if (player.GetAttrubuteManager().IsAlive() == false) {
            windowManager.ShowWindow("GameOverMenu");
        }
    }

    public DialogueManager2 GetDialogueManager() {
        return dialogueManager;
    }

    public WindowManager GetWindowManager() {
        return windowManager;
    }

    public Player GetPlayer() {
        return player;
    }

    public void RestartGame() {
        SaveManager.Instance.NewGame();
        SaveManager.Instance.SaveGame();
        SceneManager.LoadScene("GameScene");
    }

    public void LoadFromLastSave(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene() {
        SaveManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainMenu() {
        SaveManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(0);
    }

    public void EndGame() {
        SaveManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame() {
        Application.Quit();
    }

}