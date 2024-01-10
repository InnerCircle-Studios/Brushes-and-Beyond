using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class GameData {
    public PlayerData PlayerData = new();
    public SerializableDict<string, bool> Toggles = new();
    public GameData() {
    }
}