using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float knockbackStrength = 7.5f;
    PlayerStateMachine playerStateMachine;
    bool _hasAttacked;

    void Start() {
        playerStateMachine = gameObject.GetComponent<PlayerStateMachine>();
    }

    // Update is called once per frame
    void Update() {
        if (playerStateMachine.IsAttackPressed && !_hasAttacked) {
            Attack();
        }
        _hasAttacked = playerStateMachine.IsAttackPressed;
    }

    private void Attack() {
        // Find all targets in 
        RaycastHit2D[] hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, enemyLayer).Distinct().ToArray();

        foreach (RaycastHit2D hit in hits) {

            // Check if the hit object has an AttributeManager component and apply damage
            if (hit.collider.gameObject.TryGetComponent<AttributeManager2>(out var targetATM)) {
                targetATM.ApplyDamage(gameObject.GetComponent<AttributeManager2>().GetAttributes().Damage);
            }

            // Apply knockback
            if (hit.collider.gameObject.TryGetComponent<Rigidbody2D>(out var enemyRb)) {
                Vector2 knockbackDirection = (hit.collider.transform.position - transform.position).normalized;
                enemyRb.velocity += knockbackDirection * knockbackStrength;
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}
