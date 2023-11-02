using UnityEngine;

public class BrushyBounce : MonoBehaviour
{
    public float bounceAmount = 0.1f;     // The height of the bounce.
    public float bounceSpeed = 1f;        // How fast Brushy bounces.

    private Vector3 startPosition;
    private float timeCounter;

    private void Start()
    {
        startPosition = transform.position;
        timeCounter = 0;
    }

    private void Update()
    {
        timeCounter += Time.deltaTime * bounceSpeed;
        
        // Using Mathf.Abs to only get the top half of the sine wave.
        float newY = startPosition.y + Mathf.Abs(Mathf.Sin(timeCounter)) * bounceAmount;
        
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        
        // Reset timeCounter to 0 when it reaches Pi (half a wave)
        if (timeCounter > Mathf.PI)
            timeCounter = 0;
    }
}
