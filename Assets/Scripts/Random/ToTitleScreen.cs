using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitleScreen : MonoBehaviour
{
    public string targetSceneName = "Titlescene";
    public float delayInSeconds = 10f;

    void Start()
    {
        Invoke("SwitchScene", delayInSeconds);
    }

    void SwitchScene()
    {
        SaveManager.Instance.NewGame();
        SaveManager.Instance.SaveGame();
        SceneManager.LoadScene(targetSceneName);
    }
}