using System.Collections;

using UnityEngine;

public class SavePoint : MonoBehaviour {
    [SerializeField] private Animator animator;
    private Interactable interactable;
    private bool isActivated = false;

    private void Start() {
        animator = GetComponent<Animator>();
        interactable = GetComponentInChildren<Interactable>();
    }

    public void SavePointInteracted() {
        if (isActivated) {
            return;
        }
        animator.Play("SavePointSaving");
        SaveManager.Instance.SaveGame();
        StartCoroutine(SavePointActivatedCoroutine());
    }

    private IEnumerator SavePointActivatedCoroutine() {
        interactable.enabled = false;
        isActivated = true;
        yield return new WaitForSeconds(3.2f);
        animator.Play("SavePointIdle");
        isActivated = false;
        interactable.enabled = true;
    }
}