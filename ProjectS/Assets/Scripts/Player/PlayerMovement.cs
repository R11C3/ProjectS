using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    SO_Input input;

    CharacterController characterController;
    Camera mainCamera;

    Vector3 lastMovement, currentVelocity;
    Quaternion currentRotation;

    [SerializeField]
    float speed, acceleration, moveSpeed;

    Vector3 forward, right, forwardMovement, rightMovement, initialMovement, currentMovement, inputMovement;

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
    }

    void OnDisable()
    {
        input.MoveEvent -= OnMove;
    }

    // Update is called once per frame
    void Update()
    {
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

    void HandleMovement()
    {
        if ((inputMovement.x != 0 || inputMovement.y != 0) && speed <= moveSpeed)
        {
            speed += acceleration * Time.deltaTime;
        }

        if (inputMovement.x == 0 && inputMovement.y == 0 && speed > 0.0f)
        {
            speed -= acceleration * Time.deltaTime;
        }

        if (speed < 0.0f)
        {
            speed = 0.0f;
        }

        if (speed > moveSpeed)
        {
            speed = moveSpeed;
        }

        // HandleGravity();

        if (inputMovement.x == 0 && inputMovement.y == 0)
        {
            currentMovement = lastMovement;
            characterController.Move(new Vector3(currentMovement.x * speed * Time.deltaTime, currentMovement.y * Time.deltaTime, currentMovement.z * speed * Time.deltaTime));
        }

        if (inputMovement.x != 0 || inputMovement.y != 0)
        {
            characterController.Move(new Vector3(currentMovement.x * speed * Time.deltaTime, currentMovement.y * Time.deltaTime, currentMovement.z * speed * Time.deltaTime));

            lastMovement = currentMovement;
        }

        currentVelocity = characterController.velocity;
    }
}
