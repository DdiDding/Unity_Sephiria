using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.InputSystem;

public class InputComponent : GameFrameworkComponent
{
    private PlayerInputActions inputActions;

    public Vector2 Move { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }
}
