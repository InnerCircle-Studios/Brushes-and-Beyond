using System.Collections;
using UnityEngine;

public class SavePoint : MonoBehaviour {
    [SerializeField]private Animator animator;
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
        isActivated = false;
    }

    private IEnumerator SavePointActivatedCoroutine() {
        yield return new WaitForSeconds(3.2f);
        isActivated = true;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (isActivated) {
            return;
        }
        animator.Play("SavePointSaving");
        SaveManager.Instance.SaveGame();
        StartCoroutine(SavePointActivatedCoroutine());
        isActivated = false;
    }

}