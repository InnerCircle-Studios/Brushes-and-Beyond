using System;
using System.Reflection;

using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "Brushes/Actor/Attributes"), Serializable]
public class CharacterAttributes : ScriptableObject {

    public Sprite DialogueSprite;
    public float AttackRange;
    public ActorType Type;
    public string Name;
    public int MaxHealth;
    public int CurrentHealth;
    public int Damage;
    public float Speed;
    public float SprintSpeed;
    public GameObject Loot;

    public CharacterAttributes Copy() {
        CharacterAttributes newCharacterAttributes = CreateInstance<CharacterAttributes>();

        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            field.SetValue(newCharacterAttributes, field.GetValue(this));
        }

        return newCharacterAttributes;
    }
}

public enum ActorType {
    Player,
    Enemy,
    PaintEnemy,
    NPC,
    Static
}