using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogueBox : MonoBehaviour {
    [SerializeField] private SpriteRenderer avatar;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private TextMeshProUGUI message;

    public void LoadCharacter(CharacterAttributes attribs){
        avatar.sprite = attribs.DialogueSprite;
        avatarName.SetText(attribs.Name);
    }

    public void SetMessage(string message){
        this.message.SetText(message);
    }

}
