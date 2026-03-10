using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private Transform[] groundCheckPoints;
    [SerializeField] private float groundCheckRadius = 0.25f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private Animator animator;

    public bool IsGrounded  { get => _isGrounded; set => _isGrounded = value; }

    private bool isGrounded_t;

    private void Update()
    {
        // If a single ground check count as true, then set IsGrounded to True.
        isGrounded_t = false;
        foreach (var point in groundCheckPoints)
        {
            if(Physics.CheckSphere(point.position, groundCheckRadius, groundLayer))
                isGrounded_t = true;
        }
        IsGrounded = isGrounded_t;

        if(IsGrounded) 
            animator.SetBool("InAir", false);
        else
            animator.SetBool("InAir", true);
    }


    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        foreach (var point in groundCheckPoints)
        {
            if (point == null) return;

            Gizmos.color = IsGrounded ? Color.green : Color.red;

            Gizmos.DrawWireSphere(
                point.position,
                groundCheckRadius
            );
        }
        
    }
    #endif

}