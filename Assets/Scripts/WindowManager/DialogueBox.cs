using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogueBox : MonoBehaviour {
    [SerializeField] private Image avatar;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private TextMeshProUGUI message;

    public void LoadBox(DialogueBox box){
        avatar = box.avatar;
        avatarName = box.avatarName;
        message = box.message;
    }

    public void LoadCharacter(Actor actor) {
        CharacterAttributes attribs = actor.GetAttrubuteManager().GetAttributes();
        avatar.sprite = attribs.DialogueSprite;
        avatarName.SetText(attribs.Name);
    }

    public void SetMessage(string message) {
        this.message.SetText(message);
    }

}
