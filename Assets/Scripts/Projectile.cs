using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public float maxLifetime = 5f; // Maximum time before the projectile gets destroyed

    private Vector2 target;
    private GameObject player;

    void Start()
    {
        // Destroy the projectile after a certain time
        Destroy(gameObject, maxLifetime);

        // Find the player GameObject
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Set the target position to the player's position at the moment of projectile's instantiation
        if (player != null)
        {
            target = player.transform.position;
        }
    }

    void Update()
    {
        // Move towards the target
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Rotate the projectile
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // If the projectile reaches the target point (considering some small threshold), destroy it
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if it hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call the Damage function on the player
            Debug.Log("Player hit!");

            // Destroy the projectile
            Destroy(gameObject);
        }
        // Check if it hits anything else
        else if (!collision.gameObject.CompareTag("Projectile")) // To prevent self-collision
        {
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
