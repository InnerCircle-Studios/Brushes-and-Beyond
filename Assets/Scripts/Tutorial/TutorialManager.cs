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
    private bool _hasMoved, _hasRun, _hasAttacked, _hasInteracted, _tutorialCompleted, _tutorialStarted = false;
    public CanvasGroup tutorialPanel;
    public float transitionSpeed = 1.0f;

    private bool tutorialCompleted = false;

    public void MovementCheck() {
        if (!_hasMoved) {
            _moveImage.sprite = _moveGreen;
            _hasMoved = true;
        }
    }

    public void RunCheck() {
        if (!_hasRun) {
            _runImage.sprite = _runGreen;
            _hasRun = true;
        }
    }

    public void AttackCheck() {
        if (!_hasAttacked) {
            _attackImage.sprite = _attackGreen;
            _hasAttacked = true;
        }
    }

    public void InteractCheck() {
        if (!_hasInteracted) {
            _interactImage.sprite = _interactGreen;
            _hasInteracted = true;
        }
    }

    public void Start() {
        tutorialPanel.alpha = 0;
        tutorialPanel.blocksRaycasts = false;
        tutorialPanel.interactable = false;
    }

    public void Update() {
        if (_tutorialStarted) {
            CompletionCheck();
            PanelTransitions();
        }
    }

    public void TurtorialStart() {
        _tutorialStarted = true;
    }

    private void CompletionCheck() {
        if (_hasMoved && _hasRun && _hasAttacked && _hasInteracted && !_tutorialCompleted) {
            _tutorialCompleted = true;
        }
    }

    private void PanelTransitions() {
        if (!_tutorialCompleted) {
            // Fade in the panel if the tutorial hasn't been completed.
            if (tutorialPanel.alpha < 1) {
                tutorialPanel.alpha += Time.deltaTime * transitionSpeed;
                if (tutorialPanel.alpha > 0.5f) // Make the panel interactive after it's halfway visible.
                {
                    tutorialPanel.blocksRaycasts = true;
                    tutorialPanel.interactable = true;
                }
            }
        }
        else {
            // Fade out the panel once the tutorial is completed.
            if (tutorialPanel.alpha > 0) {
                tutorialPanel.alpha -= Time.deltaTime * transitionSpeed;
                if (tutorialPanel.alpha < 0.5f) // Make the panel non-interactive when it starts fading out.
                {
                    tutorialPanel.blocksRaycasts = false;
                    tutorialPanel.interactable = false;
                }
            }
        }
    }

}