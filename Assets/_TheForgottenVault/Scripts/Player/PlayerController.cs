using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator  animator;

    private CharacterController controller;
    private PlayerInputHandler input;

    private Vector3 velocity;

    private float verticalVelocity;
    private PlayerGroundCheck groundCheck;
    private PlayerJump jump;
    private PlayerDash dash;

    private void Awake()
    {
        groundCheck = GetComponent<PlayerGroundCheck>();
        jump = GetComponent<PlayerJump>(); 
        dash = GetComponent<PlayerDash>();
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;

       

        jump.HandleJump(ref verticalVelocity);

        ApplyGravity();

        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 move = input.MoveInput;

        Vector3 moveDir = new Vector3(move.x, 0, move.y);

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        moveDir = camForward * move.y + camRight * move.x;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                config.rotationSpeed * Time.deltaTime
            );
        }

        Vector3 targetVelocity = moveDir * config.moveSpeed;

        velocity = Vector3.Lerp(
            velocity,
            targetVelocity,
            config.acceleration * Time.deltaTime
        );

        Vector3 finalMove = velocity;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);

        animator.SetFloat("Speed", (Math.Abs(controller.velocity.x) + Math.Abs(controller.velocity.z)) / 2);
    }

    private void ApplyGravity()
    {
        if (groundCheck.IsGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gameConfig.gravity * Time.deltaTime;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            transform.position,
            transform.position + transform.forward * 2f
        );

        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            transform.position,
            transform.position + velocity
        );
    }
    #endif
}