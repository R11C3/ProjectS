using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerStatistics))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    SO_Input input;

    Animator animator;
    PlayerStatistics playerStatistics;

    int variations = 3;
    int activeVariation = 0;
    bool canAttack = true;
    bool stillAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playerStatistics = GetComponent<PlayerStatistics>();
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
        animator.Play("Club Attack One");
        StartCoroutine(AttackDelay());
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

    IEnumerator AttackDelay()
    {
        playerStatistics.canMove = false;
        yield return new WaitForSeconds(0.5f);
        playerStatistics.canMove = true;
    }
}
