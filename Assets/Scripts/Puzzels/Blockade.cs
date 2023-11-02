using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Blockade : MonoBehaviour {

    private bool _waitOver = false;
    private bool _usedPaints = false;
    private bool _nearPlayer = false;
    public DialogueTrigger trigger;
    public void onPaintsUsed() {
        if (_nearPlayer) {
            _usedPaints = true;
        }
    }

    private IEnumerator WaitForPaints() //Delay for groundCheck
    {
        yield return new WaitForSeconds(2f);
        _waitOver = true;
    }

    public void StartBlockadeDialogue(){
        trigger.StartDialogue();
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            _nearPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            _nearPlayer = false;
        }
    }
}