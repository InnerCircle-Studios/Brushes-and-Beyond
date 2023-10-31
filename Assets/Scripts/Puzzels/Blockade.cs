using UnityEngine;
using System.Collections;

public class Blockade : MonoBehaviour {

    private bool waitOver = false;
    private bool usedPaints = false;
    public void OnPaintsUsed() {
        usedPaints = true;
    }

    private IEnumerator WaitForPaints() { //Delay for groundCheck
        yield return new WaitForSeconds(2f);
        waitOver = true;
    }

    public void Update() {
        if (usedPaints) {
            StartCoroutine(WaitForPaints());
            if (waitOver) {
                Destroy(gameObject);
                waitOver = false;
                usedPaints = false;
            }
        }

    }
}