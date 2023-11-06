using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour {
    public GameObject Bullet;

    // Update is called once per frame
    void Update() {
        AimAtTarget(SearchForTarget());
        if (Input.GetKeyDown(KeyCode.R)) {
            Shoot();
        }
    }

    private void AimAtTarget(GameObject target) {
        if (target != null) {
            // Calculate the direction to the target
            Vector2 direction = target.transform.position - transform.position;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the object towards the target
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private GameObject SearchForTarget() {
        return FindAnyObjectByType<PlayerStateMachine>().transform.gameObject;
    }

    public void Shoot() {
        Debug.Log("Shoot");
        var newBullet = Instantiate(Bullet, transform.GetChild(0).transform.position + transform.forward * 2.0f, transform.rotation, transform.parent);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.right * 10;
    }

    public void Melee(Vector2 position, AttributeManager2 attributeManager) {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, 1.5f, transform.right, 0f, LayerMask.GetMask("Player")).Distinct().ToArray();

        foreach (RaycastHit2D hit in hits) {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.transform.CompareTag("Player")) {
                // Check if the hit object has an AttributeManager component and apply damage
                if (hit.collider.gameObject.TryGetComponent<AttributeManager2>(out var targetATM)) {
                    targetATM.ApplyDamage(attributeManager.GetAttributes().Damage);
                }

                // Apply knockback
                Rigidbody2D enemyRb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                if (enemyRb != null) {
                    Vector2 knockbackDirection = (hit.collider.transform.position - transform.position).normalized;
                    enemyRb.velocity += knockbackDirection * 7.5f;
                }
            }
        }
    }
}
