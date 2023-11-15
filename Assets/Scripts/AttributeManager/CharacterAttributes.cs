using UnityEngine;

public class CharacterAttributes : ScriptableObject
{
    public enum Role
    {
        Player,
        Enemy,
        PaintEnemy,
        NPC,
        Static
    }

    public CharacterAttributes Copy()
    {
        CharacterAttributes newCharacterAttributes = CreateInstance<CharacterAttributes>();

        newCharacterAttributes._Type = _Type;
        newCharacterAttributes._Name = _Name;
        newCharacterAttributes._MaxHealth = _MaxHealth;
        newCharacterAttributes._CurrentHealth = _CurrentHealth;
        newCharacterAttributes._Damage = _Damage;
        newCharacterAttributes._Speed = _Speed;
        newCharacterAttributes._Loot = _Loot;

        return newCharacterAttributes;
    }

    public Role _Type;

    public string _Name;

    public int _MaxHealth;

    public int _CurrentHealth;

    public int _Damage;

    public float _Speed;

    public GameObject _Loot;
}