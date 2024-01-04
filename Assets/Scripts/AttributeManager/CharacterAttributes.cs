using System;
using System.Reflection;

using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "Brushes/Actor/Attributes"), Serializable]
public class CharacterAttributes : ScriptableObject {

    public CharacterData Attributes;

    public CharacterAttributes Copy() {
        CharacterAttributes newCharacterAttributes = CreateInstance<CharacterAttributes>();

        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            field.SetValue(newCharacterAttributes, field.GetValue(this));
        }

        return newCharacterAttributes;
    }
}

[Serializable]
public class CharacterData {
    [SerializeField] public Sprite DialogueSprite;
    [SerializeField] public float AttackRange;
    [SerializeField] public ActorType Type;
    [SerializeField] public string Name;
    [SerializeField] public int MaxHealth;
    [SerializeField] public int CurrentHealth;
    [SerializeField] public int Damage;
    [SerializeField] public float Speed;
    [SerializeField] public float SprintSpeed;
    [SerializeField] public GameObject Loot;
}

public enum ActorType {
    Player,
    Enemy,
    PaintEnemy,
    NPC,
    Static
}