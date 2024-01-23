using System;
using System.Reflection;

using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "Brushes/Actor/Attributes"), Serializable]
public class CharacterAttributes : ScriptableObject {

    public CharacterData CharData;

    public CharacterAttributes Copy() {
        CharacterAttributes newCharacterAttributes = CreateInstance<CharacterAttributes>();
        newCharacterAttributes.CharData = CharData.Copy();
        return newCharacterAttributes;
    }
}

[Serializable]
public class CharacterData {
    [Header("General")]
    [SerializeField] public EmotionsContainer Emotions;
    [SerializeField] public ActorType Type;
    [SerializeField] public string Name;
    [SerializeField] public int Level;

    [Header("Combat")]
    [SerializeField] public int MaxHealth;
    [SerializeField] public int CurrentHealth;
    [SerializeField] public int Damage;
    [SerializeField] public float AttackRange;
    [SerializeField] public GameObject Loot;

    [Header("Movement")]
    [SerializeField] public float Speed;
    [SerializeField] public float SprintSpeed;

    [Header("Player specific")]
    /**
     * This is purely here becouse unity is stupid about inheritance/overriding
     */
    [SerializeField, Range(0, 3)] public int PaintCount;

    public CharacterData Copy() {
        CharacterData newCharacterData = new();
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            field.SetValue(newCharacterData, field.GetValue(this));
        }
        return newCharacterData;
    }
    public Sprite GetSprite(DialogueActorMood mood) {
        return Emotions.GetSprite(mood);
    }
}

[Serializable]
public class EmotionsContainer {
    [SerializeField] public Sprite DialogueSprite;

    [SerializeField] public Sprite HappySprite;
    [SerializeField] public Sprite NeutralSprite;
    [SerializeField] public Sprite SadSprite;
    [SerializeField] public Sprite AngrySprite;
    [SerializeField] public Sprite ConfusedSprite;
    [SerializeField] public Sprite ScaredSprite;


    public Sprite GetSprite(DialogueActorMood mood) {
        if (mood == DialogueActorMood.HAPPY && HappySprite != null) {
            return HappySprite;
        }
        else if (mood == DialogueActorMood.NEUTRAL && NeutralSprite != null) {
            return NeutralSprite;
        }
        else if (mood == DialogueActorMood.SAD && SadSprite != null) {
            return SadSprite;
        }
        else if (mood == DialogueActorMood.ANGRY && AngrySprite != null) {
            return AngrySprite;
        }
        else if (mood == DialogueActorMood.CONFUSED && ConfusedSprite != null) {
            return ConfusedSprite;
        }
        else if (mood == DialogueActorMood.SCARED && ScaredSprite != null) {
            return ScaredSprite;
        }
        else {
            return DialogueSprite;
        }
    }
}


public enum ActorType {
    PLAYER,
    ENEMY,
    PAINT_ENEMY,
    NPC,
    STATIC
}