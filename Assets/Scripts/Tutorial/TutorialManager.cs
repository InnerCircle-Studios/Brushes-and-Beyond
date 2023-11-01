using UnityEngine;
using UnityEngine.UI;
using System;

public class TutorialManager : MonoBehaviour {
    public Image _moveImage;
    public Image _runImage;
    public Image _attackImage;
    public Image _interactImage;

    public Sprite _moveGreen;
    public Sprite _runGreen;
    public Sprite _attackGreen;
    public Sprite _interactGreen;
    private bool _hasMoved, _hasRun, _hasAttacked, _hasInteracted,  _tutorialCompleted = false;

    public void MovementCheck(){
        if(!_hasMoved){
            _moveImage.sprite = _moveGreen;
            _hasMoved = true;
        }
    }

    public void RunCheck(){
        if(!_hasRun){
            _runImage.sprite = _runGreen;
            _hasRun = true;
        }
    }

    public void AttackCheck(){
        if(!_hasAttacked){
            _attackImage.sprite = _attackGreen;
            _hasAttacked = true;
        }
    }

    public void InteractCheck(){
        if(!_hasInteracted){
            _interactImage.sprite = _interactGreen;
            _hasInteracted = true;
        }
    }

    public void Update(){
        CompletionCheck();
    }

    private void CompletionCheck(){
        if(_hasMoved && _hasRun && _hasAttacked && _hasInteracted &&!_tutorialCompleted){
            _tutorialCompleted = true;
        }
    }

}