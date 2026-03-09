using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool JumpHeld { get; private set; }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += ctx =>
            MoveInput = ctx.ReadValue<Vector2>();

        inputActions.Player.Move.canceled += ctx =>
            MoveInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx =>
            JumpPressed = true;

        inputActions.Player.Jump.canceled += ctx =>
        {
            JumpPressed = false;
            JumpHeld = false;
        };
    }

    private void Update()
    {
        JumpHeld = inputActions.Player.Jump.IsPressed();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}