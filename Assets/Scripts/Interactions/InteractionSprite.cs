using UnityEngine;

public class InteractionSprite : MonoBehaviour {

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.25f, 0.25f, 0.25f));
    }
}