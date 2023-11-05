using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // Array of prefabs to spawn
    public float spawnInterval = 1f; // Time between each spawn
    public float minX = -7f; // Minimum x position
    public float maxX = 7f; // Maximum x position

    private void Start()
    {
        StartCoroutine(SpawnRandomPrefab());
    }

    private IEnumerator SpawnRandomPrefab()
    {
        while (true) // Infinite loop to keep spawning prefabs
        {
            SpawnPrefab();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnPrefab()
    {
        if (prefabs.Length == 0)
            return;

        // Randomly pick a prefab
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        // Randomly pick a position
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), transform.position.y, transform.position.z);
        // Spawn the prefab at the random position
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Move the spawner to a new x position
        transform.position = new Vector3(Random.Range(minX, maxX), transform.position.y, transform.position.z);
    }
}
