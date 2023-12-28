using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    [SerializeField] private SpawnType type;

    public SpawnPoint(SpawnType type) {
        this.type = type;
    }

    public SpawnType GetSpawnType(){
        return type;
    }

}

public enum SpawnType {
    ENEMY,
    PLAYER,
    NPC
}