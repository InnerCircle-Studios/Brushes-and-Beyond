using System;

[Serializable]
public class GameData {
    public bool NewSceneLoaded = false;
    public PlayerData PlayerData = new();
    public NPCData NPCData = new();
    public ObjectData ObjectData = new();
    public UIData UIData = new();
    public SerializableDict<string, string> QuestData = new();



    public GameData() { }
}