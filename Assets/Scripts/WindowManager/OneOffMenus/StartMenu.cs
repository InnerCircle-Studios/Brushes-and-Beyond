using SuperTiled2Unity;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour, ISaveable {
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    private string lastSceneLoaded = "GameScene";

    private void Start() {
        if (!SaveManager.Instance.HasGameData()) {
            continueButton.interactable = false;
        }
    }


    public void OnNewGame() {
        DisableButtons();
        Logger.Log("Startmenu", "Starting new game");

        // Loads the default savestate, overwriting existing files.
        SaveManager.Instance.NewGame();

        SaveManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void OnContinue() {
        DisableButtons();
        Logger.Log("Startmenu", "Loading Savefile");

        // Works w/ Savemanager OnSceneLoaded() to load the game.
        SaveManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(lastSceneLoaded);
    }

    public void QuitGame() {
        DisableButtons();

        #if UNITY_WEBGL
            Logger.Log("QuitGame Conditional","WebGL build detected, redirecting to itch.io page");
            SaveManager.Instance.NewGame();
            SaveManager.Instance.SaveGame();
            Application.OpenURL("https://innercircles.itch.io/brushes-and-beyond");
        #else
            Logger.Log("QuitGame Conditional","Default build detected, quitting game");
            Application.Quit();
        #endif
        // if(Application.platform == RuntimePlatform.WebGLPlayer) {
        //     Logger.Log("QuitGame Conditional","WebGL build detected, redirecting to itch.io page");
        //     SaveManager.Instance.NewGame();
        //     SaveManager.Instance.SaveGame();
        //     Application.OpenURL("https://innercircles.itch.io/brushes-and-beyond");
        // }
        // else {
        //     Logger.Log("QuitGame Conditional","Default build detected, quitting game");
        //     Application.Quit();
        // }
    }

    public void DisableButtons() {
        newGameButton.interactable = false;
        continueButton.interactable = false;
    }

    public void LoadData(GameData data) {
        if (!data.PlayerData.SceneName.IsEmpty()) {
            lastSceneLoaded = data.PlayerData.SceneName;
        }
    }

    public void SaveData(GameData data) {
    }
}
