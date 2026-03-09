using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    private PlayerGroundCheck groundCheck;
    private PlayerInputHandler input;

    private float jumpTimer;
    private bool isJumping;

    public bool IsJumping => isJumping;

    private void Awake()
    {
        groundCheck = GetComponent<PlayerGroundCheck>();
        input = GetComponent<PlayerInputHandler>();
    }

    public void HandleJump(ref float verticalVelocity)
    {
        if (groundCheck.IsGrounded && input.JumpPressed)
        {
            isJumping = true;
            jumpTimer = 0f;

            verticalVelocity = config.jumpForce;
        }

        if (isJumping && input.JumpHeld)
        {
            if (jumpTimer < config.maxJumpTime)
            {
                verticalVelocity += config.jumpHoldForce * Time.deltaTime;
                jumpTimer += Time.deltaTime;
            }
        }

        if (!input.JumpHeld)
        {
            isJumping = false;
        }
    }
}