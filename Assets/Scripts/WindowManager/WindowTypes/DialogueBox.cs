using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogueBox : MonoBehaviour {
    [SerializeField] private Image avatar;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private Button nextButton;

    public void LoadBox(DialogueBox box) {
        avatar = box.avatar;
        avatarName = box.avatarName;
        message = box.message;
    }

    public void LoadCharacter(Actor actor) {
        CharacterAttributes attribs = actor.GetAttrubuteManager().GetAttributes();
        avatar.sprite = attribs.DialogueSprite;
        avatarName.SetText(attribs.Name);
        HandleCharacters(actor);
    }

    public void SetMessage(string message) {
        this.message.SetText(message);
    }

    private void HandleCharacters(Actor actor) {
        //TODO add logic to switch the box to the left or right side of the screen.
        if (actor.GetAttrubuteManager().GetAttributes().Type == ActorType.Player) {
            // switch the box to the left side of the screen.
        }
        else {
            // switch the box to the right side of the screen.
        }

    }

}
