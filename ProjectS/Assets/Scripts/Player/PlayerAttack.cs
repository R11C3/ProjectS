using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    SO_Input input;

    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        input.AttackEvent += OnAttack;
        input.AttackCanceledEvent += OnAttackCanceled;
    }

    void OnDisable()
    {
        input.AttackEvent -= OnAttack;
        input.AttackCanceledEvent -= OnAttackCanceled;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAttack()
    {
        animator.SetBool("attacking", true);
    }

    void OnAttackCanceled()
    {
        animator.SetBool("attacking", false);
    }
}
