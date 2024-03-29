using Unity.VisualScripting;

using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public float maxLifetime = 5f; // Maximum time before the projectile gets destroyed

    private Vector2 target;
    private GameObject player;

    void Start() {
        Destroy(gameObject, maxLifetime);
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            target = player.transform.position;
        }
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        // Check if it hits the player
        if (collision.gameObject.CompareTag("Player")) {
            Logger.Log("DamageHandler", collision.gameObject.name + " was hit by " + gameObject.name);
            PlayRandomAttackSound();
            collision.gameObject.GetComponent<Player>().GetAttrubuteManager().ApplyDamage(1);
            //StartCoroutine(collision.gameObject.GetComponent<Player>().FlashSpriteOnHit(collision.gameObject.GetComponent<SpriteRenderer>()));

            Destroy(gameObject);
        }
    }

    public void PlayRandomAttackSound() {
        int random = Random.Range(0, 5);
        switch (random) {
            case 0:
                AudioManager.instance.PlaySfx("HitEffect1");
                break;
            case 1:
                AudioManager.instance.PlaySfx("HitEffect2");
                break;
            case 2:
                AudioManager.instance.PlaySfx("HitEffect3");
                break;
            case 3:
                AudioManager.instance.PlaySfx("HitEffect4");
                break;
            case 4:
                AudioManager.instance.PlaySfx("HitEffect5");
                break;
        }
    }

}
