using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    private CharacterController controller;
    private PlayerInputHandler input;

    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;

        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 move = input.MoveInput;

        Vector3 moveDir = new Vector3(move.x, 0, move.y);

        Vector3 targetVelocity = moveDir * config.moveSpeed;

        velocity = Vector3.Lerp(
            velocity,
            targetVelocity,
            config.acceleration * Time.deltaTime
        );

        controller.Move(velocity * Time.deltaTime);
    }
}