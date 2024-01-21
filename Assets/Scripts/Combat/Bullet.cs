using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bullet : MonoBehaviour {
    public float KnockbackStrength = 2.0f; // Set this to whatever strength you want for the bullet knockback
    float _spawnTime;

    void Start() {
        _spawnTime = Time.time;
    }

    void Update() {
        if (Time.time - _spawnTime > 4) {
            Destroy(transform.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.gameObject.TryGetComponent<AttributeManager2>(out var targetATM)) {

            // Knockback logic for player
            if (other.transform.CompareTag("Player")) { // Assuming player has tag "Player"
                targetATM.ApplyDamage(transform.parent.GetComponent<AttributeManager2>().GetAttributes().Damage);
                if (other.gameObject.TryGetComponent<Rigidbody2D>(out var playerRb)) {

                    Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                    playerRb.velocity += knockbackDirection * KnockbackStrength;
                }
            }
        }

        if (!other.transform.CompareTag(transform.parent.tag)) {
            Destroy(transform.gameObject);
        }
    }
}
