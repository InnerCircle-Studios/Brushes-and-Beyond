using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxEffectMultiplier = 0.5f;

    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        
        // Check if the camera has moved
        if (deltaMovement.magnitude > 0)
        {
            transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, 0, 0);
            lastCameraPosition = cameraTransform.position;
        }

        // Optional: Check if we need to loop the background
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
}
