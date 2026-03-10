using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    private PlayerInputHandler input;

    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;
    public bool IsDashing => isDashing;

    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
    }

    public void HandleDash(ref Vector3 velocity)
    {
        if(!config.allowDash)
            return;

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        if (!isDashing && dashCooldownTimer <= 0 && input.DashPressed)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            velocity = transform.forward * config.dashSpeed;

            if (dashTimer <= 0)
            {
                EndDash();
            }
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = config.dashDuration;
        dashCooldownTimer = config.dashCooldown;

        if(GameManager.Instance.DebugMode) Debug.Log("Dash");
    }

    private void EndDash()
    {
        isDashing = false;
    }
}