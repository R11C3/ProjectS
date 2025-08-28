using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    SO_Input input;
    [SerializeField]
    SO_Player player;

    CharacterController characterController;
    Camera mainCamera;

    Vector3 lastMovement, currentVelocity;
    Quaternion currentRotation;

    [SerializeField]
    float speed, gravity, timeFalling;

    [SerializeField]
    AnimationCurve jumpCurve;

    Vector3 forward, right, forwardMovement, rightMovement, initialMovement, currentMovement, inputMovement;

    bool isJumping, canJump;

    public float dampening;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        mainCamera = Camera.main;

        lastMovement = Vector3.zero;

        CalibrateMovement();
    }

    void OnEnable()
    {
        input.MoveEvent += OnMove;

        input.JumpEvent += OnJump;
    }

    void OnDisable()
    {
        input.MoveEvent -= OnMove;

        input.JumpEvent -= OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        HandleGravity();
        HandleMovement();
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
        CalibrateMovement();
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
            StartCoroutine(JumpRoutine());
        }
    }

    void HandleMovement()
    {
        if ((inputMovement.x != 0 || inputMovement.y != 0) && speed <= player.moveSpeed)
        {
            speed += player.acceleration * Time.deltaTime;
        }

        if (inputMovement.x == 0 && inputMovement.y == 0 && speed > 0.0f)
        {
            speed -= player.acceleration * Time.deltaTime;
        }

        if (speed < 0.0f)
        {
            speed = 0.0f;
        }

        if (speed > player.moveSpeed)
        {
            speed = player.moveSpeed;
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

    IEnumerator JumpRoutine()
    {
        float elapsedTime = 0.0f;
        float activeForce = player.jumpForce;

        while (elapsedTime < player.jumpTime)
        {
            characterController.Move(new Vector3(0.0f, jumpCurve.Evaluate(elapsedTime) * activeForce * Time.deltaTime, 0.0f));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        isJumping = false;

        yield return null;
    }
}
