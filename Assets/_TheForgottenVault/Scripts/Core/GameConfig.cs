using UnityEngine;

[CreateAssetMenu(menuName = "Configs/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("World Config")]
    public float gravity = 6f;
}

