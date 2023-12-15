using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
    public UnityEvent OnEventTrigger = new();

    private SpriteRenderer activationKey;

    private void Start() {
        activationKey = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ActivateIndicator(){
        activationKey.enabled = true;
    }

    public void DeactivateIndicator(){
        activationKey.enabled = false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .3f);
    }
}