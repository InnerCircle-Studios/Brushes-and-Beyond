using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class SaveManager : MonoBehaviour {
    [Header("Saving config")]
    [SerializeField] private string filename = "brushes";
    [SerializeField] private bool useEncryption = false;

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
        }
    }

    private void Start() {
        dataManager = new FileDataManager(Application.persistentDataPath, filename + ".WDF", useEncryption);
        saveables = FindAllSaveables();
        LoadGame();    // Remove after testing
    }

    public void NewGame() {
        gameData = new GameData();
    }

    public void LoadGame() {
        Logger.Log("SaveManager", "Loading game, looking for saves.");
        gameData = dataManager.Load();

        if (gameData == null) {
            Logger.Log("SaveManager", "No game data found. Loading defaults");
            NewGame();
        }
        saveables.ForEach(saveable => saveable.LoadData(gameData));
    }

    public void SaveGame() {
        Logger.Log("SaveManager", "Saving game.");
        saveables.ForEach(saveable => saveable.SaveData(gameData));
        dataManager.Save(gameData);
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    private List<ISaveable> FindAllSaveables() {
        List<ISaveable> saveables = new();
        saveables.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>());
        saveables.AddRange(FindObjectsOfType<Player>().OfType<ISaveable>());
        return saveables;
    }
}