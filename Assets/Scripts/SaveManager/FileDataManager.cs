using System;
using System.IO;

using UnityEngine;

public class FileDataManager {
    private string dataDir = "";
    private string fileName = "";
    private bool useEncryption = false;
    private readonly string encryptionKey = "666*Blood_For_The_Blood_God*666";

    public FileDataManager(string dataDir, string fileName, bool useEncryption) {
        this.dataDir = dataDir;
        this.fileName = fileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load() {
        string savePath = Path.Combine(dataDir, fileName);
        try {
            if (File.Exists(savePath)) {
                Logger.Log("FileDataManager", $"Save file found at {savePath}");
                using FileStream stream = new(savePath, FileMode.Open);
                using StreamReader reader = new(stream);
                string json = reader.ReadToEnd();
                if (useEncryption) {
                    json = EncryptDecrypt(json);
                }
                return JsonUtility.FromJson<GameData>(json);
            }
            else {
                Logger.Log("FileDataManager", $"No save file found at {savePath}");
            }
        }
        catch (Exception e) {
            Logger.LogError("FileDataManager", $"Error loading from {savePath}!\n{e.Message}");
        }

        return null;
    }

    public void Save(GameData data) {
        string savePath = Path.Combine(dataDir, fileName);

        try {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));

            string jsonToSave = JsonUtility.ToJson(data, true);

            if (useEncryption) {
                jsonToSave = EncryptDecrypt(jsonToSave);
            }
            using FileStream stream = new(savePath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(jsonToSave);
            Logger.Log("FileDataManager", $"Saved data to: {savePath}");
        }
        catch (Exception e) {

            Logger.LogError("FileDataManager", $"Error saving to {savePath}!\n{e.Message}");
        }
    }

    private string EncryptDecrypt(string data) {
        string result = "";
        for (int i = 0; i < data.Length; i++) {
            result += (char)(data[i] ^ encryptionKey[(i % encryptionKey.Length)]);
        }
        return result;
    }

}