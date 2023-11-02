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

    public void DialogueBrushy() {
        if (!_firstDialogue) {
            FirstDialogue.StartDialogue();
        }
        if (_firstDialogue && !_secondDialogue) {
            SecondDialogue.StartDialogue();
        }
        if (_firstDialogue && _secondDialogue && !_thirdDialogue) {
            _thirdDialogue = true;
            SpawnPaints(new Vector2(35,-35));
            Debug.Log("ThirdDialoguePlaying");
            ThirdDialogue.StartDialogue();
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

    public void SpawnPaints(Vector2 startPosition) {
        float objectWidth = objectPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        Vector2 position1 = startPosition;
        Vector2 position2 = position1 + new Vector2(objectWidth+ 2, 2);
        Vector2 position3 = position2 + new Vector2(objectWidth + 2, 0);

        // Instantiate objects
        Instantiate(objectPrefab, position1, Quaternion.identity);
        Instantiate(objectPrefab, position2, Quaternion.identity);
        Instantiate(objectPrefab, position3, Quaternion.identity);

    }

}
