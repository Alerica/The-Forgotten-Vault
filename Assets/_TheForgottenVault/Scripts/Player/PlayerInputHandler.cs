using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore;
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool JumpHeld { get; private set; }
    public bool DashPressed { get; private set; }
    public bool AttackPressed { get; private set; }

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

        inputActions.Player.Dash.performed += ctx => 
            DashPressed = true;

        inputActions.Player.Attack.performed += ctx =>
            AttackPressed = true;
    }

    private void Update()
    {
        JumpHeld = inputActions.Player.Jump.IsPressed();
        DashPressed = false;
        AttackPressed = false;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}