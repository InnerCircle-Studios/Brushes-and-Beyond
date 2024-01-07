using System.Data;

using UnityEngine;

public class ArcProjectile2D : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab; // Assign in Unity inspector
    [SerializeField] private float duration = 3f; // Total duration of the flight

    private Vector2 startPosition;
    private Vector2 midPosition;
    private Vector2 endPosition;
    private float elapsedTime;

    private void Start() {
        startPosition = transform.position;
        endPosition = new Vector2(startPosition.x + RandomOffset(), startPosition.y - 3); // End 5 units to the right (change this to whatever you want
        midPosition = (startPosition + endPosition) / 2 + new Vector2(0, 6f); // Midpoint + 3 units up

        elapsedTime = 0f;
    }

    private void Update() {
        elapsedTime += Time.deltaTime;

        float t = elapsedTime / duration; // Normalized time
        if (t > 1f) {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        // Parabolic interpolation
        Vector2 position = ParabolicLerp(startPosition, midPosition, endPosition, t);
        transform.position = position;


    }

    private Vector2 ParabolicLerp(Vector2 start, Vector2 mid, Vector2 end, float t) {
        // Quadratic bezier curve: B(t) = (1-t)^2 * P0 + 2(1-t)t * P1 + t^2 * P2
        return (1 - t) * (1 - t) * start + 2 * (1 - t) * t * mid + t * t * end;
    }

    int RandomOffset() {
        return Random.Range(0, 2) == 0 ? -3 : 3;
    }
}
