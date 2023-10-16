using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bullet : MonoBehaviour {
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
        }
        if(!other.transform.CompareTag(transform.parent.tag)){
            Destroy(transform.gameObject);
        }
    }
}
