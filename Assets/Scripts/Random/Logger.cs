using UnityEngine;

public static class Logger {
    public static void Log(string name, string message) {
        Debug.Log($"<color=#ffffff>[  <color=#5eff33>{name}</color>  ]: " + message+"</color>");
    }

    public static void LogError(string name, string message) {
        Debug.LogError($"[ <color=#ff0000>{name}</color>  ]: " + message);
    }
}