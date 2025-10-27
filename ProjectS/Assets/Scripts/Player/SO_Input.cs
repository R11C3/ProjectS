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
    InputAction sprintAction;
    InputAction crouchAction;
    InputAction attackAction;
    InputAction interactAction;

    public event UnityAction<Vector2> MoveEvent;

    public event UnityAction JumpEvent;
    public event UnityAction JumpCanceledEvent;

    public event UnityAction SprintEvent;
    public event UnityAction SprintCanceledEvent;

    public event UnityAction CrouchEvent;
    public event UnityAction CrouchCanceledEvent;

    public event UnityAction AttackEvent;
    public event UnityAction AttackCanceledEvent;

    public event UnityAction InteractEvent;
    public event UnityAction InteractCanceledEvent;

    void OnEnable()
    {
        moveAction = inputActions.FindAction("Move");
        jumpAction = inputActions.FindAction("Jump");
        sprintAction = inputActions.FindAction("Sprint");
        crouchAction = inputActions.FindAction("Crouch");
        attackAction = inputActions.FindAction("Attack");
        interactAction = inputActions.FindAction("Interact");

        moveAction.started += OnMoveInput;
        moveAction.performed += OnMoveInput;
        moveAction.canceled += OnMoveInput;

        jumpAction.started += OnJumpInput;
        jumpAction.canceled += OnJumpInput;

        sprintAction.started += OnSprintInput;
        sprintAction.canceled += OnSprintInput;

        crouchAction.started += OnCrouchInput;
        crouchAction.canceled += OnCrouchInput;

        attackAction.started += OnAttackInput;
        attackAction.canceled += OnAttackInput;

        interactAction.started += OnInteractInput;
        interactAction.canceled += OnInteractInput;

        moveAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        crouchAction.Enable();
        attackAction.Enable();
        interactAction.Enable();
    }

    void OnDisable()
    {
        moveAction.started -= OnMoveInput;
        moveAction.performed -= OnMoveInput;
        moveAction.canceled -= OnMoveInput;

        jumpAction.started -= OnJumpInput;
        jumpAction.canceled -= OnJumpInput;

        sprintAction.started -= OnSprintInput;
        sprintAction.canceled -= OnSprintInput;

        crouchAction.started -= OnCrouchInput;
        crouchAction.canceled -= OnCrouchInput;

        attackAction.started -= OnAttackInput;
        attackAction.canceled -= OnAttackInput;

        interactAction.started -= OnInteractInput;
        interactAction.canceled -= OnInteractInput;

        moveAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        crouchAction.Disable();
        attackAction.Disable();
        interactAction.Disable();
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

    void OnSprintInput(InputAction.CallbackContext context)
    {
        if (SprintEvent != null && context.started)
        {
            SprintEvent.Invoke();
        }
        if (SprintCanceledEvent != null && context.canceled)
        {
            SprintCanceledEvent.Invoke();
        }
    }

    void OnCrouchInput(InputAction.CallbackContext context)
    {
        if (CrouchEvent != null && context.started)
        {
            CrouchEvent.Invoke();
        }
        if (CrouchCanceledEvent != null && context.canceled)
        {
            CrouchCanceledEvent.Invoke();
        }
    }

    void OnAttackInput(InputAction.CallbackContext context)
    {
        if (AttackEvent != null && context.started)
        {
            AttackEvent.Invoke();
        }
        if (AttackCanceledEvent != null && context.canceled)
        {
            AttackCanceledEvent.Invoke();
        }
    }

    void OnInteractInput(InputAction.CallbackContext context)
    {
        if (InteractEvent != null && context.started)
        {
            InteractEvent.Invoke();
        }
        if (InteractCanceledEvent != null && context.canceled)
        {
            InteractCanceledEvent.Invoke();
        }
    }
}
