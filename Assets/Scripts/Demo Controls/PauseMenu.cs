using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;
    private bool _isPaused = false;
    private bool _isPausePressed = false;

    void Start() {
        pauseMenu.SetActive(false);
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            // Toggle the paused state
            if (_isPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }

            Debug.Log("Pause pressed");
        }
    }

}