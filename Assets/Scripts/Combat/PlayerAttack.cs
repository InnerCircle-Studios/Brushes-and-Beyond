using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private Transform _attackTransform;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private LayerMask _enemyLayer;
    PlayerStateMachine _playerStateMachine;
    bool _hasAttacked;

    void Start() {
        _playerStateMachine = gameObject.GetComponent<PlayerStateMachine>();
    }

    // Update is called once per frame
    void Update() {
        if (_playerStateMachine.IsAttackPressed && !_hasAttacked) {
            Attack();
        }
        _hasAttacked = _playerStateMachine.IsAttackPressed;
    }

    private void Attack() {
        // Find all targets in 
        RaycastHit2D[] hits = Physics2D.CircleCastAll(_attackTransform.position, _attackRange, transform.right, 0f, _enemyLayer).Distinct().ToArray();

        foreach (RaycastHit2D hit in hits) {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.transform.parent.gameObject.TryGetComponent<AttributeManager>(out var targetATM)) {
                targetATM.ApplyDamage(gameObject.GetComponent<AttributeManager>().Damage);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(_attackTransform.position, _attackRange);
    }
}
