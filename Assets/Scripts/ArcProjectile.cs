using UnityEngine;
using System.Collections;

public class ArcProjectile : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign this in the Unity inspector
    public Vector2 initialVelocity;
    public Rigidbody2D rb;
    private Animator _animator;
    public bool launched = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (launched)
        {
            Launch();
            StartCoroutine(SpawnEnemyAfterDelay());
        }
    }

    public void Launch()
    {
        rb.velocity = initialVelocity;
    }

    private IEnumerator SpawnEnemyAfterDelay()
    {
        // Wait for a random time between 3 and 6 seconds
        float delay = Random.Range(3.0f, 6.0f);
        yield return new WaitForSeconds(delay);

        // Instantiate the enemy and destroy the projectile
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}