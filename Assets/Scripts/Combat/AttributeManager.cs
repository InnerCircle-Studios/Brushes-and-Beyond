using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AttributeManager : MonoBehaviour {

    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _currentHealth;
    [SerializeField]
    private int _damage;

    // public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value > 0 ? value : 0; } }
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value >= 0 ? value : 0; } }
    public int Damage { get { return _damage; } set { _damage = value >= 0 ? value : 0; } }

    void Update() {
        if (!IsAlive()) {
            if(transform.gameObject.TryGetComponent<SpriteRenderer>(out var renderer)){
                renderer.color = Color.red;
            }
            
            foreach(SpriteRenderer rendererr in transform.GetComponentsInChildren<SpriteRenderer>()){
                rendererr.color = Color.red;
            }
        }

    }

    /// <summary>
    /// Method <c>ApplyDamage<c> Reduces the current hitpoints to a minimum of 0.
    /// </summary>
    /// <param name="hp"></param>
    public void ApplyDamage(int hp) {
        if (_currentHealth - hp < 0) {
            _currentHealth = 0;
        }
        else {
            _currentHealth -= hp;
        }
        // _currentHealth = _currentHealth - hp >= 0 ? _currentHealth - hp : 0;
    }

    /// <summary>
    /// Method <c>ApplyHeal<c> Adds the given amount of hitpoints to the current hitpoints.
    /// The amount of current hitpoints cannot be more than the max hitpoints.
    /// </summary>
    /// <param name="hp"></param>
    public void ApplyHeal(int hp) {
        if (_currentHealth + hp > _maxHealth) {
            _currentHealth = _maxHealth;
        }
        else {
            _currentHealth += hp;
        }
        // _currentHealth = _currentHealth + hp > _maxHealth ? _currentHealth + hp : _maxHealth;
    }

    /// <summary>
    /// Method <c>DealDamage<c> Applies the damage specified in _damage to the target's AttributeManager.
    /// </summary>
    /// <param name="target"></param>
    public void DealDamage(GameObject target) {
        if (target.TryGetComponent<AttributeManager>(out var atm)) {
            atm.ApplyDamage(_damage);
        }
    }

    /// <summary>
    /// Method <c>IsAlive<c> Checks if the AttributeManager has more than 0 current hitpoints.
    /// </summary>
    /// <returns></returns>
    public bool IsAlive() {
        return _currentHealth > 0;
    }

}
