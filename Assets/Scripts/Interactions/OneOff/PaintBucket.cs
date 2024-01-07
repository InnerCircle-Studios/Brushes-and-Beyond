using UnityEngine;

public class PaintBucket : MonoBehaviour {

    public string BucketSet;

    private void Start() {
        OnHideObject(BucketSet);
    }

    private void OnEnable() {
        InteractionEvents.OnShowObject += OnShowObject;
        InteractionEvents.OnHideObject += OnHideObject;
    }

    private void OnDisable() {
        InteractionEvents.OnShowObject -= OnShowObject;
        InteractionEvents.OnHideObject -= OnHideObject;
    }

    private void OnShowObject(string name) {
        if (name == BucketSet) {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponentInChildren<Interactable>().enabled = true;
        }
    }
    private void OnHideObject(string name) {
        if (name == BucketSet) {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponentInChildren<Interactable>().enabled = false;
        }
    }

    public void OnActivation() {
        InteractionEvents.ActivatePaintBucket(1);
        Destroy(gameObject);
    }
}