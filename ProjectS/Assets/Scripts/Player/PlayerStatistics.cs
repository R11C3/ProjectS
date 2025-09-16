using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public SO_Player template;

    [Header("Basic Movement Stats")]
    public float moveSpeed;
    public float acceleration;
    public float sprintSpeedMultiplier;
    public float crouchSpeedMultiplier;
    public bool canMove = true;

    [Header("Jump Stats")]
    public float jumpForce;
    public float jumpTime;

    void Awake()
    {
        moveSpeed = template.moveSpeed;
        acceleration = template.acceleration;
        sprintSpeedMultiplier = template.sprintSpeedMultiplier;
        crouchSpeedMultiplier = template.crouchSpeedMultiplier;
        jumpForce = template.jumpForce;
        jumpTime = template.jumpTime;
    }
}
