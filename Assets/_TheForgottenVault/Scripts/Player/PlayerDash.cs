using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    private CharacterController controller;
    private PlayerInputHandler input;

    private bool isDashing;
    private bool isInvincible;

    public bool IsDashing => isDashing;
    public bool IsInvincible => isInvincible;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        if (!config.allowDash) return;

        if (input.DashPressed && !isDashing)
        {
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;
        isInvincible = true;

        Vector3 dashDirection = transform.forward;
        debugDashDirection = dashDirection;

        float timer = 0f;


        while (timer < config.dashDuration)
        {
            controller.Move(
                dashDirection * config.dashForce * Time.deltaTime
            );

            timer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(config.dashInvincibilityTime);

        isInvincible = false;
    }

    #if UNITY_EDITOR
    private Vector3 debugDashDirection;

    private void OnDrawGizmos()
    {
        if (debugDashDirection == Vector3.zero) return;

        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(
            transform.position,
            transform.position + debugDashDirection * 2f
        );
    }
    #endif
}