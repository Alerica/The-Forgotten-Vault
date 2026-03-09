using System.ComponentModel;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius = 0.25f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private bool _isGrounded;

    public bool IsGrounded  { get => _isGrounded; set => _isGrounded = value; }

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(
            groundCheckPoint.position,
            groundCheckRadius,
            groundLayer
        );
    }


    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (groundCheckPoint == null) return;

        Gizmos.color = IsGrounded ? Color.green : Color.red;

        Gizmos.DrawWireSphere(
            groundCheckPoint.position,
            groundCheckRadius
        );
    }
    #endif

}