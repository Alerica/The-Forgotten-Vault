using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    [SerializeField] private float invincibilityTime = 0.6f;

    public int currentHP;
    private float invincibleTimer;

    public int CurrentHP => currentHP;

    public bool IsInvincible => invincibleTimer > 0;

    private void Awake()
    {
        currentHP = maxHP;
    }

    private void Update()
    {
        if (invincibleTimer > 0)
            invincibleTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage, Vector3 knockbackDirection, float knockbackForce)
    {
        if (IsInvincible)
            return;

        currentHP -= damage;

        Debug.Log("Player took damage: " + damage);

        PlayerKnockback knockback = GetComponent<PlayerKnockback>();
        if (knockback != null)
            knockback.ApplyKnockback(knockbackDirection, knockbackForce);

        invincibleTimer = invincibilityTime;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");

        GameManager.Instance.GameOver();
    }
}