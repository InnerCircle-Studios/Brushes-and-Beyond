using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;

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
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void QuitGame() {
        DisableButtons();
        Application.Quit();
    }

    public void DisableButtons() {
        newGameButton.interactable = false;
        continueButton.interactable = false;
    }
}
