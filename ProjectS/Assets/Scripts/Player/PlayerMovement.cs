using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(PlayerStatistics))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    SO_Input input;
    [SerializeField]
    PlayerStatistics player;

    CharacterController characterController;
    Animator animator;
    Camera mainCamera;

    Vector3 lastMovement, currentVelocity;
    Quaternion currentRotation;

    [SerializeField]
    float newMoveSpeed, newAcceleration, gravity, timeFalling, rotationSpeed;

    [SerializeField]
    AnimationCurve jumpCurve;

    Vector3 forward, right, forwardMovement, rightMovement, initialMovement, currentMovement, inputMovement;

    bool canJump;
    public bool isJumping, crouched;
    public float speed, direction;

    public float dampening;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerStatistics>();

        mainCamera = Camera.main;

        lastMovement = Vector3.zero;

        newMoveSpeed = player.moveSpeed;
        newAcceleration = player.acceleration;

        CalibrateMovement();
    }

    void OnEnable()
    {
        input.MoveEvent += OnMove;
        input.JumpEvent += OnJump;
        input.SprintEvent += OnSprint;
        input.SprintCanceledEvent += OnSprintCanceled;
        input.CrouchEvent += OnCrouch;
        input.CrouchCanceledEvent += OnCrouchCanceled;
    }

    void OnDisable()
    {
        input.MoveEvent -= OnMove;
        input.JumpEvent -= OnJump;
        input.SprintEvent -= OnSprint;
        input.SprintCanceledEvent -= OnSprintCanceled;
        input.CrouchEvent -= OnCrouch;
        input.CrouchCanceledEvent -= OnCrouchCanceled;
    }

    // Update is called once per frame
    void Update()
    {
        HandleGravity();
        
        if (player.canMove)
        {
            HandleMovement();
        }

        animator.SetFloat("linearSpeed", speed);

    }

    void CalibrateMovement()
    {
        forward = mainCamera.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f)) * forward;
    }

    void OnMove(Vector2 inputMovement)
    {
        rightMovement = right * inputMovement.x;
        forwardMovement = forward * inputMovement.y;
        initialMovement = Vector3.Normalize(rightMovement + forwardMovement);
        currentMovement = Vector3.Lerp(currentMovement, initialMovement, dampening);
        this.inputMovement = inputMovement;
    }

    void OnJump()
    {
        isJumping = true;
        if (canJump)
        {
            canJump = false;
            // StartCoroutine(JumpRoutine());
        }
    }

    void OnCrouch()
    {
        newMoveSpeed = player.moveSpeed * player.crouchSpeedMultiplier;
        crouched = true;
        animator.SetBool("crouched", crouched);
    }

    void OnCrouchCanceled()
    {
        newMoveSpeed = player.moveSpeed;
        crouched = false;
        animator.SetBool("crouched", crouched);
    }

    void OnSprint()
    {
        newMoveSpeed = player.moveSpeed * player.sprintSpeedMultiplier;
        newAcceleration = player.acceleration * 3.0f;
    }

    void OnSprintCanceled()
    {
        newMoveSpeed = player.moveSpeed;
        newAcceleration = player.acceleration;
    }

    void HandleMovement()
    {
        if ((inputMovement.x != 0 || inputMovement.y != 0) && speed <= newMoveSpeed)
        {
            speed += newAcceleration * Time.deltaTime;
        }

        if (inputMovement.x == 0 && inputMovement.y == 0 && speed > 0.0f)
        {
            speed -= newAcceleration * Time.deltaTime;
        }

        if (speed < 0.0f)
        {
            speed = 0.0f;
        }

        if (speed > newMoveSpeed)
        {
            speed = newMoveSpeed;
        }

        if (inputMovement.x == 0 && inputMovement.y == 0)
        {
            currentMovement = lastMovement;
        }

        if (inputMovement.x != 0 || inputMovement.y != 0)
        {
            lastMovement = currentMovement;
        }

        characterController.Move(new Vector3(currentMovement.x * speed * Time.deltaTime, gravity * Time.deltaTime, currentMovement.z * speed * Time.deltaTime));

        currentVelocity = characterController.velocity;

        HandleRotation();
    }

    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            canJump = true;
            gravity = -0.05f;
            timeFalling = 0;
        }
        if (!characterController.isGrounded && !isJumping && currentMovement.y > -10.0f)
        {
            gravity = -(jumpCurve.Evaluate(0.9f - timeFalling) * 10.0f);
            timeFalling += Time.deltaTime;
        }

        characterController.Move(new Vector3(0, gravity * Time.deltaTime, 0));
    }

    void HandleRotation()
    {
        if (currentMovement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(currentMovement.x, 0.0f, currentMovement.z));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
