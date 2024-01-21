using System.Collections;
using Cinemachine;
using UnityEngine;

[AddComponentMenu("")] // Hides the script from the Add Component menu in the editor
[RequireComponent(typeof(CinemachineVirtualCamera))] // Ensure there is a CinemachineVirtualCamera
public class CameraShake : CinemachineExtension
{
    public float ShakeDuration = 2f;
    public float ShakeAmplitude = 2.0f;
    public float ShakeFrequency = 10.0f;

    private float ShakeElapsedTime = 0f;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (ShakeElapsedTime > 0)
        {
            // Only apply shake at the Aim stage
            if (stage == CinemachineCore.Stage.Aim)
            {
                Vector2 shakeOffset = Random.insideUnitCircle * ShakeAmplitude;
                state.PositionCorrection += (Vector3)shakeOffset;
                ShakeElapsedTime -= deltaTime;
            }
        }
        else
        {
            // Make sure we do not leave any leftover shake
            ShakeElapsedTime = 0;
            state.PositionCorrection = Vector3.zero;
        }
    }

    public void StartShake()
    {
        ShakeElapsedTime = ShakeDuration;
    }
}
