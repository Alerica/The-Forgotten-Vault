using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CameraConfig")]
public class CameraConfig : ScriptableObject
{
    [Header("Camera Setting")]
    public float sensitivityX = 300f;
    public float sensitivityY = 2f;
    public float cameraDistance = 5f;
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 60f;
}