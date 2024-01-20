using System.Collections;

using UnityEngine;

public class SavePoint : MonoBehaviour {
    [SerializeField] private Animator animator;
    private bool isActivated = false;

    private void Start() {
        animator = GetComponent<Animator>();
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
        isActivated = true;
        yield return new WaitForSeconds(3.2f);
        animator.Play("SavePointIdle");
        isActivated = false;
    }
}