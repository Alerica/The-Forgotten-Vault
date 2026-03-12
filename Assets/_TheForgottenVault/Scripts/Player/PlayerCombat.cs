using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackCooldown = 0.5f;
    private float attackTimer;

    private void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
    }

    public void Attack()
    {
        if(GameManager.Instance.DebugMode) Debug.Log("Attack");

        if (attackTimer > 0)
        return;

        attackTimer = attackCooldown;

        Collider[] hits = Physics.OverlapSphere(
            attackPoint.position,
            attackRadius,
            enemyLayer
        );

        foreach (var hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
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