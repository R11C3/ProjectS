using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerStatistics))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    SO_Input input;

    Animator animator;
    PlayerStatistics playerStatistics;

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
        animator.SetBool("attacking", false);
    }

    IEnumerator AttackDelay()
    {
        playerStatistics.canMove = false;
        yield return new WaitForSeconds(0.5f);
        playerStatistics.canMove = true;
    }
}
