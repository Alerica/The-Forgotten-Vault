using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float acceleration = 10f;
    public float deceleration = 12f;

    [Header("Jump")]
    public float jumpForce = 7f;
    public float jumpHoldForce = 2f;
    public float maxJumpTime = 0.2f;

    [Header("Dash")]
    public bool allowDash = true;
    public float dashForce = 12f;
    public float dashDuration = 0.2f;

    [Header("Combat")]
    public int maxHP = 100;
    public int attackDamage = 20;
    public float attackDuration = 1f;
    public float attackCooldown = 1f;
}