using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private LayerMask playerLayer;

    private float attackTimer;

    private void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
    }

    public void TryAttack()
    {
        if (attackTimer > 0)
            return;

        Collider[] hits = Physics.OverlapSphere(
            attackPoint.position,
            attackRadius,
            playerLayer
        );

        foreach (var hit in hits)
        {
            PlayerHealth health = hit.GetComponent<PlayerHealth>();

            if (health != null)
            {
                Vector3 knockDir = (hit.transform.position - transform.position).normalized;

                health.TakeDamage(
                    config.attackDamage,
                    knockDir,
                    config.knockbackForce
                );
            }

            if(GameManager.Instance.DebugMode) Debug.Log($"Enemy {gameObject} hit player with {config.attackDamage}");
        }

        attackTimer = config.attackCooldown;
    }

    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (attackPoint == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    #endif
}