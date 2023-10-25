using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AttributeManager2 : MonoBehaviour {

    [SerializeField] private CharacterAttributes _attributes;

    private void Start() {

    }

    private void Update() {

    }

    public void ApplyDamage(int hp) {
        // Force hp to be at least 0
        if (_attributes.CurrentHealth - hp < 0) {
            _attributes.CurrentHealth = 0;
        }
        else {
            _attributes.CurrentHealth -= hp;
        }
    }

    public void ApplyHeal(int hp) {
        if (_attributes.CurrentHealth + hp > _attributes.MaxHealth) {
            _attributes.CurrentHealth = _attributes.MaxHealth;
        }
        else {
            _attributes.CurrentHealth += hp;
        }
    }
}
