using UnityEngine;
using System.Collections;

public class Blockade : MonoBehaviour {

    private bool _waitOver = false;
    private bool _usedPaints = false;
    public void onPaintsUsed() {
        _usedPaints = true;
    }

    private IEnumerator WaitForPaints() //Delay for groundCheck
    {
        yield return new WaitForSeconds(2f);
        _waitOver = true;
    }

    public void Update() {
        if (_usedPaints) {
            StartCoroutine(WaitForPaints());
            if (_waitOver) {
                Destroy(gameObject);
                _waitOver = false;
                _usedPaints = false;
            }
        }

    }
}