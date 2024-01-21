using UnityEngine;

public class PauseCheck : MonoBehaviour
{
    public GameObject panel; // Assign your panel GameObject in the inspector

    void Update()
    {
        // Check if the panel is active in the scene
        if (panel.activeInHierarchy)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0; // Pauses the game
        // Any additional pause logic can go here
    }

    void ResumeGame()
    {
        Time.timeScale = 1; // Resumes the game
        // Any additional resume logic can go here
    }
}
