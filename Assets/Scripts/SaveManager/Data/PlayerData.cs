using System;

using UnityEngine;

[Serializable]
public class PlayerData {
    public Vector3 PlayerPosition;
    public CharacterData PlayerAttributes;
    public string SceneName = "";

    public PlayerData() {
        PlayerPosition = Vector3.zero;
        PlayerAttributes = new() {
            Type = ActorType.STATIC
        };
    }
}