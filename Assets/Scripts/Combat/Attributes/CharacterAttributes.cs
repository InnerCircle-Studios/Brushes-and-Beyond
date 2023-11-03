using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "Brushes/Character/Attributes")]
public class CharacterAttributes : ScriptableObject {
    public enum Role {
        Player,
        Enemy,
        PaintEnemy,
        NPC,
        Static
    }

    public Role Type;
    public string Name;
    public int MaxHealth;
    public int CurrentHealth;
    public int Damage;

    public CharacterAttributes Copy() {
        CharacterAttributes newAts = CreateInstance<CharacterAttributes>();

        // Use reflection to dynamically get every property in the current class and copy it to a new object
        foreach(PropertyInfo prop in GetType().GetProperties()){
            object value = prop.GetValue(this);
            prop.SetValue(newAts, value);
        }

        return newAts;

        // return new() {
        //     Type = Type,
        //     Name = Name,
        //     MaxHealth = MaxHealth,
        //     CurrentHealth = CurrentHealth,
        //     Damage = Damage
        // };
    }
}
