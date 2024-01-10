using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class GameData {
    public Vector3 PlayerPosition;
    public SerializableDict<string, bool> Toggles = new();
    public GameData() {
        PlayerPosition = Vector3.zero;
    }
}