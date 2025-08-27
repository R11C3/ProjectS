using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "SO_Input", menuName = "Scriptable Objects/SO_Input")]
public class SO_Input : ScriptableObject
{
    [SerializeField]
    InputActionAsset inputActions;

    InputAction moveAction;

    public event UnityAction<Vector2> MoveEvent;

    void OnEnable()
    {
        moveAction = inputActions.FindAction("Move");

        moveAction.started += OnMoveInput;
        moveAction.performed += OnMoveInput;
        moveAction.canceled += OnMoveInput;

        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.started -= OnMoveInput;
        moveAction.performed -= OnMoveInput;
        moveAction.canceled -= OnMoveInput;

        moveAction.Disable();
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
}
