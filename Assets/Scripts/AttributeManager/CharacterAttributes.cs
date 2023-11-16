using UnityEngine;

public class CharacterAttributes : ScriptableObject {

    public Role Type;

    public string Name;

    public int MaxHealth;

    public int CurrentHealth;

    public int Damage;

    public float Speed;

    public GameObject Loot;

    public enum Role {
        Player,
        Enemy,
        PaintEnemy,
        NPC,
        Static
    }

    public CharacterAttributes Copy() {
        CharacterAttributes newCharacterAttributes = CreateInstance<CharacterAttributes>();

        newCharacterAttributes.Type = Type;
        newCharacterAttributes.Name = Name;
        newCharacterAttributes.MaxHealth = MaxHealth;
        newCharacterAttributes.CurrentHealth = CurrentHealth;
        newCharacterAttributes.Damage = Damage;
        newCharacterAttributes.Speed = Speed;
        newCharacterAttributes.Loot = Loot;

        return newCharacterAttributes;
    }
}