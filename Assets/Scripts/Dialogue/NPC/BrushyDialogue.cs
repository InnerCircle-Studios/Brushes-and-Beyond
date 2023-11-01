using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushyDialogue : MonoBehaviour
{
    public DialogueTrigger FirstDialogue;
    public DialogueTrigger SecondDialogue;
    public DialogueTrigger ThirdDialogue;
    private bool _firstDialogue = false;
    private bool _secondDialogue = false;
    private bool _thirdDialogue = false;





    public void DialogueBrushy(){
        if(!_firstDialogue){
            FirstDialogue.StartDialogue();
    }
        if(_firstDialogue && !_secondDialogue){
            SecondDialogue.StartDialogue();
    }
        if(_firstDialogue && _secondDialogue && !_thirdDialogue){
            ThirdDialogue.StartDialogue();
    }

    }

    public void DestroyBrushy(){
        Destroy(gameObject);
    }
    public void TutorialCompleted(){
        _firstDialogue = true;
    }

    public void SecondDialogueCompleted(){
        _secondDialogue = true;
    }

}
