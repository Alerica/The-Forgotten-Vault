using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    [Header("Health")]
    public int maxHP = 100;

    [Header("Movement")]
    public float moveSpeed = 3.5f;
    public float rotationSpeed = 120f;
    public float stoppingDistance = 2f;

    [Header("Detection")]
    public float detectionRadius = 10f;

    [Header("Combat")]
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;

    [Header("Knockback")]
    public float knockbackForce = 4f;
}