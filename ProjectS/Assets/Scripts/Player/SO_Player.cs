using UnityEngine;

[CreateAssetMenu(fileName = "SO_Player", menuName = "Scriptable Objects/SO_Player")]
public class SO_Player : ScriptableObject
{
    [Header("Basic Movement Stats")]
    public float moveSpeed;
    public float acceleration;
    public float sprintSpeedMultiplier;
    public float crouchSpeedMultiplier;
    
    [Header("Jump Stats")]
    public float jumpForce;
    public float jumpTime;
}
