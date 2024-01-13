using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class GameData {
    public PlayerData PlayerData = new();
    public NPCData NPCData = new();
    public ObjectData ObjectData = new();
    public SerializableDict<string, string> QuestData = new();


    public GameData() { }
}