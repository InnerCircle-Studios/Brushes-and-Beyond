using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    [SerializeField] private SpawnType type;

    public SpawnPoint(SpawnType type) {
        this.type = type;
    }

    public SpawnType GetSpawnType() {
        return type;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

}

public enum SpawnType {
    ENEMY,
    PLAYER,
    NPC
}