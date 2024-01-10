using System;

using UnityEngine;

[Serializable]
public class PlayerData {
    public Vector3 PlayerPosition;

    public PlayerData() {
        PlayerPosition = Vector3.zero;
    }
}