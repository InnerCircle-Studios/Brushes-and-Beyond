using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TileCameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine camera
    private float tileWidth; // The width of a tile
    private float tileHeight; // The height of a tile

    private Vector3 lastTilePosition; // Store the position of the last tile the player was in

    void Start()
    {
        // Set tile dimensions based on the camera's orthographic size and aspect ratio
        tileHeight = virtualCamera.m_Lens.OrthographicSize * 2;
        tileWidth = tileHeight * ((float)Screen.width / (float)Screen.height);

        // Initially set the camera to the player's position
        SnapToTile(player.position);
        lastTilePosition = virtualCamera.transform.position;
    }


    void Update()
    {
        Vector3 currentTile = new Vector3(
            Mathf.Floor(player.position.x / tileWidth) * tileWidth + tileWidth / 2,
            Mathf.Floor(player.position.y / tileHeight) * tileHeight + tileHeight / 2,
            virtualCamera.transform.position.z // Keep the same z-position
        );

        // Check if the player has moved to a new tile
        if (currentTile != lastTilePosition)
        {
            SnapToTile(currentTile);
            lastTilePosition = currentTile;
        }
    }

    void SnapToTile(Vector3 tileCenter)
    {
        tileCenter.z = -10; // Set the z-position to -10
        virtualCamera.transform.position = tileCenter;
    }
}
