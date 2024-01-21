using UnityEngine;
using System.Collections;

public class Blockade : MonoBehaviour {

    private bool _waitOver = false;
    private bool _usedPaints = false;
    private bool _nearPlayer = false;
    private bool hasSoundPlayed = false;
    public DialogueTrigger trigger;
    public void onPaintsUsed() {
        if (_nearPlayer) {
            _usedPaints = true;
        }
    }

    private IEnumerator WaitForPaints() {
        Debug.Log("WaitForPaints");
        yield return new WaitForSeconds(2f);
        _waitOver = true;
    }

    public void StartBlockadeDialogue() {
        if (_nearPlayer) {
            Debug.Log("StartBlockadeDialogue");
            trigger.StartDialogue();
        }
    }

    public void Update() {
        if (_usedPaints) {
            StartCoroutine(WaitForPaints());
            if (!hasSoundPlayed) {
                AudioManager.instance.PlaySfx("Lightning");
                hasSoundPlayed = true;
            }
            if (_waitOver) {
                AudioManager.instance.StopSfx("Lightning");
                Destroy(gameObject);
                _waitOver = false;
                _usedPaints = false;
                hasSoundPlayed = false;
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