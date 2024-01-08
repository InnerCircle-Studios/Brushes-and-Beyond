using UnityEngine;

public class Blockade : MonoBehaviour {
    [SerializeField] private string blockadeName;
    private void Start() {
        InteractionEvents.OnShowObject += OnShowObject;
        InteractionEvents.OnHideObject += OnHideObject;
    }

    private void OnDestroy() {
        InteractionEvents.OnShowObject -= OnShowObject;
        InteractionEvents.OnHideObject -= OnHideObject;
    }

    private void OnShowObject(string name) {
        if (name == blockadeName) {
            gameObject.SetActive(true);
        }
    }

    private void OnHideObject(string name) {
        if (name == blockadeName) {
            gameObject.SetActive(false);
        }
    }
}