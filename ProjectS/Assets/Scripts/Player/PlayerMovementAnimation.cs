using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerMovementAnimation : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator animator;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
