using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;

    public int currentHP;

    private void Awake()
    {
        currentHP = config.maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}