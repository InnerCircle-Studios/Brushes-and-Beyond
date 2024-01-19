using Unity.VisualScripting;

using UnityEngine;

public class PaintBucket : MonoBehaviour, ISaveable {

    [Header("EventSelector")]
    [SerializeField] private string bucketSet;

    [Header("Settings")]
    [SerializeField] private bool hideByDefault = true;
    [SerializeField] private bool healOnPickup = false;
    [SerializeField] private int healAmount = 5;

    private void Start() {
        if (hideByDefault) {
            OnHideObject(bucketSet);
        }
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
        if (name == bucketSet) {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponentInChildren<Interactable>().enabled = true;
        }
    }
    private void OnHideObject(string name) {
        if (name == bucketSet) {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponentInChildren<Interactable>().enabled = false;
        }
    }

    public void OnActivation() {
        InteractionEvents.ActivatePaintBucket(1);
        AudioManager.instance.PlaySfx("BucketPickUp");
        if (healOnPickup) {
            GameManager.Instance.GetPlayer().GetAttrubuteManager().ApplyHeal(healAmount);
        }
        gameObject.SetActive(false);
    }

    public void LoadData(GameData data) {
        if (data.ObjectData.Toggles.TryGetValue(gameObject.name, out bool value) && value) {
            OnShowObject(bucketSet);
        }
    }

    public void SaveData(GameData data) {
        data.ObjectData.Toggles[gameObject.name] = gameObject.activeSelf && gameObject.GetComponent<SpriteRenderer>().enabled;
    }
}