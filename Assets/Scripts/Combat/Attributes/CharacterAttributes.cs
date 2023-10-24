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
}
