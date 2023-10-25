using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AttributeManager2 : MonoBehaviour {

    [SerializeField] private CharacterAttributes _attributes;
    private CharacterAttributes _localAttributes;

    private void Start() {
        _localAttributes = ScriptableObject.CreateInstance<CharacterAttributes>();
        _localAttributes.Type = _attributes.Type;
        _localAttributes.CurrentHealth = _attributes.CurrentHealth;
        _localAttributes.MaxHealth = _attributes.MaxHealth;
        _localAttributes.Damage = _attributes.Damage;
        _localAttributes.name = _attributes.name;   
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
