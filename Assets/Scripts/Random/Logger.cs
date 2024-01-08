using UnityEngine;

public static class Logger {
    public static void Log(string name, string message) {
        Debug.Log($"<color=silver>[  <color=lime>{name}</color>  ]: " + message+"</color>");
    }

    public static void LogError(string name, string message) {
        Debug.LogError($"<color=white>[ <color=#red>{name}</color>  ]: " + message+"</color>");
    }
}