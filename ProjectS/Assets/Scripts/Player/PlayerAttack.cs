using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    SO_Input input;

    Animator animator;

    GameObject meleeWeapon;
    MeshCollider meleeCollider;

    int variations = 3;
    int variationCount = 0;
    bool canAttack = true;
    bool stillAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        meleeCollider = meleeWeapon.GetComponent<MeshCollider>();
        meleeCollider.enabled = false;
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
        stillAttacking = true;
        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackRoutine());
        }
    }

    void OnAttackCanceled()
    {
        stillAttacking = false;
    }

    IEnumerator AttackRoutine()
    {
        string animationName = "Club Attack " + ((variationCount % variations) + 1);
        animator.Play(animationName);

        yield return new WaitForSeconds(1f);

        if (stillAttacking)
        {
            variationCount++;
            StartCoroutine(AttackRoutine());
        }
        else
        {
            animator.Play("Club Attack End");
        }

        canAttack = true;

        yield return null;
    }
}
