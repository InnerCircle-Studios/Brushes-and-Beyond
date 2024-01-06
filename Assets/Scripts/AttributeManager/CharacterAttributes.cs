using System;
using System.Reflection;

using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "Brushes/Actor/Attributes"), Serializable]
public class CharacterAttributes : ScriptableObject {

    public CharacterData Attributes;

    public CharacterAttributes Copy() {
        CharacterAttributes newCharacterAttributes = CreateInstance<CharacterAttributes>();
        newCharacterAttributes.Attributes = Attributes.Copy();
        return newCharacterAttributes;
    }
}

[Serializable]
public class CharacterData {
    [Header("General")]
    [SerializeField] public Sprite DialogueSprite;
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

    public CharacterData Copy() {
        CharacterData newCharacterData = new();
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            field.SetValue(newCharacterData, field.GetValue(this));
        }
        return newCharacterData;
    }
}

public enum ActorType {
    Player,
    Enemy,
    PaintEnemy,
    NPC,
    Static
}