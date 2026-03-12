using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private CameraConfig config;

    private CinemachineFreeLook freeLookCamera;

    private void Awake()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();

        ApplyConfig();
    }

    private void ApplyConfig()
    {
        // Sensitivity
        freeLookCamera.m_XAxis.m_MaxSpeed = config.sensitivityX;
        freeLookCamera.m_YAxis.m_MaxSpeed = config.sensitivityY;

        // Vertical clamp
        freeLookCamera.m_YAxis.m_MinValue = config.minVerticalAngle;
        freeLookCamera.m_YAxis.m_MaxValue = config.maxVerticalAngle;

        // Distance
        SetCameraDistance(config.cameraDistance);
    }

    private void SetCameraDistance(float distance)
    {
        for (int i = 0; i < 3; i++)
        {
            var orbit = freeLookCamera.m_Orbits[i];
            orbit.m_Radius = distance;
            freeLookCamera.m_Orbits[i] = orbit;
        }
    }
}