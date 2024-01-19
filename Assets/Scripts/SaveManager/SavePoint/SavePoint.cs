using UnityEngine;
using System.Collections;

public class SavePoint : MonoBehaviour {
    private Animator animator;
    private bool isActivated = false;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void SavePointInteracted() {
        if (isActivated) {
            return;
        }
        animator.Play("SavePointSaving");
        SaveManager.Instance.SaveGame();
        SavePointActivatedCoroutine();
        isActivated = false;
    }

    private IEnumerator SavePointActivatedCoroutine() {
        yield return new WaitForSeconds(3.2f);
        isActivated = true;
    }

}