using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private float detectionRadius;

    [SerializeField] private LayerMask playerLayer;

    public void SetRadius(float radius)
    {
        detectionRadius = radius;
    }

    public bool CanSeePlayer()
    {
        return Physics.CheckSphere(
            transform.position,
            detectionRadius,
            playerLayer
        );
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
#endif
}