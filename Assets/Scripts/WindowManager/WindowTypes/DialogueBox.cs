using System;
using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogueBox : MonoBehaviour {
    [Header("Dialogue Box components")]
    [SerializeField] private Image avatar;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI hurryUpText;

    [Header("Settings")]
    [SerializeField] private bool toggleDialogueShifting = true;
    private Animator layoutAnimator;

    private void Awake() {
        layoutAnimator = GetComponent<Animator>();
    }

    public void LoadBox(DialogueBox box) {
        avatar = box.avatar;
        avatarName = box.avatarName;
        message = box.message;
    }

    public void LoadCharacter(Actor actor) {
        CharacterData attribs = actor.GetAttrubuteManager().GetAttributes();
        avatar.sprite = attribs.DialogueSprite;
        avatarName.SetText(attribs.Name);
        HandleCharacters(actor);
    }

    public void SetMessage(string message) {
        StopCoroutine(HurryUp()); // Cancel any running counters
        this.message.SetText(message);
        StartCoroutine(HurryUp()); // Start new counter to display the press E text.
    }

    private void HandleCharacters(Actor actor) {
        if (toggleDialogueShifting) {
            if (actor.GetAttrubuteManager().GetAttributes().Type == ActorType.PLAYER) {
                // switch the box to the left side of the screen.
                layoutAnimator.Play("left");
            }
            else {
                // switch the box to the right side of the screen.
                layoutAnimator.Play("right");
            }
        }
    }

    IEnumerator HurryUp() {
        hurryUpText.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        hurryUpText.gameObject.SetActive(true);
    }

}
