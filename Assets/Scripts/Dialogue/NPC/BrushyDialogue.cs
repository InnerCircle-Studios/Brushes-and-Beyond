using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BrushyDialogue : MonoBehaviour {
    public DialogueTrigger FirstDialogue;
    public DialogueTrigger SecondDialogue;
    public DialogueTrigger ThirdDialogue;
    public GameObject objectPrefab;
    private bool _firstDialogue = false;
    private bool _secondDialogue = false;
    private bool _thirdDialogue = false;
    private bool Collided = false;

    public void DialogueBrushy() {
        if (Collided) {
            if (!_firstDialogue) {
                FirstDialogue.StartDialogue();
            }
            if (_firstDialogue && !_secondDialogue) {
                SecondDialogue.StartDialogue();
            }
            if (_firstDialogue && _secondDialogue && !_thirdDialogue) {
                _thirdDialogue = true;
                SpawnPaints();
                Debug.Log("ThirdDialoguePlaying");
                ThirdDialogue.StartDialogue();
            }
        }
    }

    public void DestroyBrushy() {
        Destroy(gameObject);
    }
    public void TutorialCompleted() {
        _firstDialogue = true;
    }

    public void SecondDialogueCompleted() {
        _secondDialogue = true;
    }

    public void SpawnPaints() {
        float objectWidth = objectPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        Vector2 position1 = new Vector2(transform.position.x, transform.position.y - 2);
        Vector2 position2 = position1 + new Vector2(objectWidth + 1, 0);
        Vector2 position3 = position2 + new Vector2(objectWidth + 1, 0);

        // Instantiate objects
        Instantiate(objectPrefab, position1, Quaternion.identity);
        Instantiate(objectPrefab, position2, Quaternion.identity);
        Instantiate(objectPrefab, position3, Quaternion.identity);

    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Collided = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Collided = false;
        }
    }

}
