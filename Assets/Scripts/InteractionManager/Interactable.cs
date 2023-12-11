using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
    public UnityEvent OnEventTrigger = new();

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .3f);
    }
}