using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;
    [SerializeField] private Transform player;
    

    private NavMeshAgent agent;
    private EnemyStateMachine stateMachine;
    private EnemyDetection detection;
    private EnemyCombat combat;

    private Vector3 startPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();
        detection = GetComponent<EnemyDetection>();
        combat = GetComponent<EnemyCombat>();

        startPosition = transform.position;

        ApplyConfig();
    }

    private void ApplyConfig()
    {
        agent.speed = config.moveSpeed;
        agent.angularSpeed = config.rotationSpeed;
        agent.stoppingDistance = config.stoppingDistance;

        detection.SetRadius(config.detectionRadius);
    }

    public void HandleIdle()
    {
        if (detection.CanSeePlayer())
            stateMachine.ChangeState(EnemyState.Chase);
    }

    public void HandlePatrol()
    {
        if (detection.CanSeePlayer())
            stateMachine.ChangeState(EnemyState.Chase);
    }

    public void HandleChase()
    {
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= config.attackRange)
            stateMachine.ChangeState(EnemyState.Attack);

        if (!detection.CanSeePlayer())
            stateMachine.ChangeState(EnemyState.Return);
    }

    public void HandleAttack()
    {
        agent.SetDestination(transform.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= config.attackRange)
        {
            combat.TryAttack();
        }

        if (distance > config.attackRange)
        {
            stateMachine.ChangeState(EnemyState.Chase);
        }
    }

    public void HandleReturn()
    {
        agent.SetDestination(startPosition);

        if (Vector3.Distance(transform.position, startPosition) < 0.5f)
            stateMachine.ChangeState(EnemyState.Idle);
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            transform.position,
            transform.position + transform.forward * 2f
        );
    }
    #endif
}