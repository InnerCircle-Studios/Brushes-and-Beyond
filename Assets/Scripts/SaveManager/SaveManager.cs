using System.Collections.Generic;
using System.Linq;


using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {
    [Header("Saving config")]
    [SerializeField] private string filename = "brushes";
    [SerializeField] private bool useEncryption = false;
    [SerializeField] private bool togglePresistance = true;
    [SerializeField] private bool initializeDataIfNoneFound = false;

    private GameData gameData;
    private List<ISaveable> saveables;
    private FileDataManager dataManager;

    public static SaveManager Instance { get; private set; }


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Logger.LogError("SaveManager", "Multiple SaveManagers found!");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        dataManager = new FileDataManager(Application.persistentDataPath, filename + ".WDF", useEncryption);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        saveables = FindAllSaveables();
        LoadGame();
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnApplicationQuit() {
        SaveGame();
    }





    public void NewGame() {
        gameData = new GameData();
    }

    public void LoadGame() {
        if (togglePresistance) {
            Logger.Log("SaveManager", "Loading game, looking for saves.");
            gameData = dataManager.Load();

            if (gameData == null && initializeDataIfNoneFound) {
                Logger.Log("SaveManager", "No save found, initializing new game.");
                NewGame();
            }

            if (gameData == null) {
                Logger.LogError("SaveManager", "No game data found. Start a new game before loading!");
                return;
            }
            saveables.ForEach(saveable => saveable.LoadData(gameData));
        }

    }

    public void SaveGame() {
        if (togglePresistance) {
            if (gameData == null) {
                Logger.LogError("SaveManager", "No data to save. Start a new game before saving!");
                return;
            }

            Logger.Log("SaveManager", "Saving game.");
            saveables.ForEach(saveable => saveable?.SaveData(gameData));
            dataManager.Save(gameData);
        }
    }



    private List<ISaveable> FindAllSaveables() {
        List<ISaveable> saveables = new();
        saveables.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>());
        saveables.AddRange(FindObjectsOfType<Player>().OfType<ISaveable>());
        return saveables;
    }

    public bool HasGameData() {
        return gameData != null;
    }
}