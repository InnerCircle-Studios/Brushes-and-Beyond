using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AttributeManager2 : MonoBehaviour {

    [SerializeField] private CharacterAttributes attributes;
    private CharacterAttributes localAttributes;

    private void Awake() {
        localAttributes = attributes.Copy();
    }

    public void ApplyDamage(int hp) {
        // Force hp to be at least 0
        if (localAttributes.CurrentHealth - hp < 0) {
            localAttributes.CurrentHealth = 0;
        }
        else {
            localAttributes.CurrentHealth -= hp;
        }
    }

    public void ApplyHeal(int hp) {
        if (localAttributes.CurrentHealth + hp > localAttributes.MaxHealth) {
            localAttributes.CurrentHealth = localAttributes.MaxHealth;
        }
        else {
            localAttributes.CurrentHealth += hp;
        }
    }
}
