using UnityEngine;

public class Blockade : MonoBehaviour, ISaveable {

    [Header("EventSelector")]
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

    public void LoadData(GameData data) {
        if (data.ObjectData.Toggles.TryGetValue(blockadeName, out bool value)) {
            gameObject.SetActive(value);
        }
    }

    public void SaveData(GameData data) {
        data.ObjectData.Toggles[blockadeName] = gameObject.activeSelf;
    }
}