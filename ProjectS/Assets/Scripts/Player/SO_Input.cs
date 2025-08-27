using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "SO_Input", menuName = "Scriptable Objects/SO_Input")]
public class SO_Input : ScriptableObject
{
    [SerializeField]
    InputActionAsset inputActions;

    InputAction moveAction;
    InputAction jumpAction;

    public event UnityAction<Vector2> MoveEvent;

    public event UnityAction JumpEvent;
    public event UnityAction JumpCanceledEvent;

    void OnEnable()
    {
        moveAction = inputActions.FindAction("Move");
        jumpAction = inputActions.FindAction("Jump");

        moveAction.started += OnMoveInput;
        moveAction.performed += OnMoveInput;
        moveAction.canceled += OnMoveInput;

        jumpAction.started += OnJumpInput;
        jumpAction.canceled += OnJumpInput;

        moveAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        moveAction.started -= OnMoveInput;
        moveAction.performed -= OnMoveInput;
        moveAction.canceled -= OnMoveInput;

        jumpAction.started -= OnJumpInput;
        jumpAction.canceled -= OnJumpInput;

        moveAction.Disable();
        jumpAction.Disable();
    }

    void OnMoveInput(InputAction.CallbackContext context)
    {
        if (MoveEvent != null && context.started)
        {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }
        if (MoveEvent != null && context.performed)
        {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }
        if (MoveEvent != null && context.canceled)
        {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }
    }

    void OnJumpInput(InputAction.CallbackContext context)
    {
        if (JumpEvent != null && context.started)
        {
            JumpEvent.Invoke();
        }
        if (JumpCanceledEvent != null && context.canceled)
        {
            JumpCanceledEvent.Invoke();
        }
    }
}
