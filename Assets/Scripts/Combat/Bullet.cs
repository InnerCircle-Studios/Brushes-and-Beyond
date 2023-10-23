using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bullet : MonoBehaviour {
    public float knockbackStrength = 2.0f; // Set this to whatever strength you want for the bullet knockback
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
        if (other.transform.gameObject.TryGetComponent<AttributeManager>(out var targetATM)) {
            targetATM.ApplyDamage(transform.parent.GetComponent<AttributeManager>().Damage);
            
            // Knockback logic for player
            if (other.transform.CompareTag("Player")) { // Assuming player has tag "Player"
                Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null) {
                    Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                    playerRb.velocity += knockbackDirection * knockbackStrength;
                }
            }
        }

        if (!other.transform.CompareTag(transform.parent.tag)) {
            Destroy(transform.gameObject);
        }
    }
}
