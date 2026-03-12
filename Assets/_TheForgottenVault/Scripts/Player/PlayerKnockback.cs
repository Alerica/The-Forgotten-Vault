using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 knockbackVelocity;
    private float knockbackTimer;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (knockbackTimer > 0)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);
            knockbackTimer -= Time.deltaTime;
        }
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        knockbackVelocity = direction.normalized * force;
        knockbackTimer = 0.2f;

         if(GameManager.Instance.DebugMode) Debug.Log($"Knockback Applied with {force} force");
    }
}