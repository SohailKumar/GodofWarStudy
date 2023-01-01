using UnityEngine;
using UnityEngine.InputSystem;

public class HumanoidLandInput : MonoBehaviour
{
    public Vector2 moveInput { get; private set; } = Vector2.zero;
    public Vector2 lookInput { get; private set; } = Vector2.zero;

    public bool moveIsPressed = false;

    public bool invertMouseY { get; private set; } = true;

    InputActions input;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;

        input = new InputActions();
        input.HumanoidLand.Enable();

        input.HumanoidLand.Move.performed += SetMove;
        input.HumanoidLand.Move.canceled += SetMove;

        input.HumanoidLand.Look.performed += SetLook;
        input.HumanoidLand.Look.canceled += SetLook;
    }

    private void OnDisable()
    {
        input.HumanoidLand.Move.performed -= SetMove;
        input.HumanoidLand.Move.canceled -= SetMove;

        input.HumanoidLand.Look.performed -= SetLook;
        input.HumanoidLand.Look.canceled -= SetLook;

        input.HumanoidLand.Disable();

        Cursor.lockState = CursorLockMode.None;
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        moveIsPressed = !(moveInput == Vector2.zero);
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }
}
