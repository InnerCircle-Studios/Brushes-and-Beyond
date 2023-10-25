using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "Brushes/Character/Attributes")]
public class CharacterAttributes : ScriptableObject {
    public enum Role {
        Player,
        Enemy,
        NPC,
        Static
    }

    public Role Type;
    public string Name;
    public int MaxHealth;
    public int CurrentHealth;
    public int Damage;

    public CharacterAttributes NewInstance() {
        CharacterAttributes localAttributes = CreateInstance<CharacterAttributes>();
        localAttributes.Type = this.Type;
        localAttributes.CurrentHealth = this.CurrentHealth;
        localAttributes.MaxHealth = this.MaxHealth;
        localAttributes.Damage = this.Damage;
        localAttributes.name = this.name;

        return localAttributes;
    }
}
