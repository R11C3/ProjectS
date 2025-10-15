using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    SO_Input input;

    Animator animator;

    GameObject meleeWeapon;
    MeshCollider meleeCollider;

    int variations = 3;
    int activeVariation = 0;
    bool canAttack = true;
    bool stillAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        // meleeCollider = meleeWeapon.GetComponent<MeshCollider>();
        // meleeCollider.enabled = false;
        activeVariation = 0;
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
        string animationName = "Club Attack " + ((activeVariation % variations) + 1);
        animator.Play(animationName);

        float elapsedTime = 0.0f;
        float animationTime = animator.GetCurrentAnimatorClipInfo(0).Length;
        bool playNext = false;

        while (elapsedTime < animationTime + 0.05f)
        {
            if (stillAttacking && elapsedTime >= 0.5f)
            {
                playNext = true;
            }

            elapsedTime += Time.deltaTime;

            if (playNext && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                break;
            }

            yield return null;
        }

        if (playNext)
        {
            activeVariation++;
            StartCoroutine(AttackRoutine());
        }
        else
        {
            animator.Play("Club Attack End");
            activeVariation = 0;
        }

        canAttack = true;

        yield return null;
    }
}
