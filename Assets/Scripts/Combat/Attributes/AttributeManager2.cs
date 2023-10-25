using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AttributeManager2 : MonoBehaviour {

    [SerializeField] private CharacterAttributes _templateAttributes;
    private CharacterAttributes _localAttributes;

    private void Start() {
        // Copy data from the template to a new object for local, non-shared use.
        _localAttributes = _templateAttributes.NewInstance();

    }

    public void ApplyDamage(int hp) {
        // Force hp to be at least 0
        if (_localAttributes.CurrentHealth - hp < 0) {
            _localAttributes.CurrentHealth = 0;
        }
        else {
            _localAttributes.CurrentHealth -= hp;
        }
    }

    public void ApplyHeal(int hp) {
        if (_localAttributes.CurrentHealth + hp > _localAttributes.MaxHealth) {
            _localAttributes.CurrentHealth = _localAttributes.MaxHealth;
        }
        else {
            _localAttributes.CurrentHealth += hp;
        }
    }

    public CharacterAttributes GetAttributes() {
        return _localAttributes;
    }
}
