using System.Collections;

using UnityEngine;

public class Blockade : MonoBehaviour, ISaveable {

    [Header("EventSelector")]
    [SerializeField] private string blockadeName;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
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
            StartCoroutine(HideObject());
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

    private IEnumerator HideObject() {
        animator.Play("VortexRemove");
        QuestEvents.SetDialogueAdvanceable(false);
        yield return new WaitForSecondsRealtime(2.30f);
        QuestEvents.SetDialogueAdvanceable(true);
        gameObject.SetActive(false);
    }


}