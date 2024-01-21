using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraControl2D : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public Vector3 targetPosition; // Set this in the Inspector
    public float moveSpeed = 5f; // Set this in the Inspector
    private Transform originalFollowTarget;

    private void Start()
    {
        originalFollowTarget = cinemachineVirtualCamera.Follow;
    }

    public void MoveCameraToTargetPosition()
    {
        StartCoroutine(MoveCamera(targetPosition, moveSpeed));
    }

    private IEnumerator MoveCamera(Vector3 targetPos, float speed)
    {
        QuestEvents.SetDialogueAdvanceable(false);
        cinemachineVirtualCamera.Follow = null;
        Vector3 targetPosition2D = new Vector3(targetPos.x, targetPos.y, cinemachineVirtualCamera.transform.position.z);

        while (Vector3.Distance(cinemachineVirtualCamera.transform.position, targetPosition2D) > 0.01f)
        {
            cinemachineVirtualCamera.transform.position = Vector3.MoveTowards(
                cinemachineVirtualCamera.transform.position,
                targetPosition2D,
                speed * Time.deltaTime);

            yield return null;
        }
        QuestEvents.SetDialogueAdvanceable(true);
    }

    public void EnableCameraFollow()
    {
        cinemachineVirtualCamera.Follow = originalFollowTarget;
    }
}
