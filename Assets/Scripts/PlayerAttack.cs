using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [Header("ATTACK")]
    [SerializeField] Collider2D attackCollider;
    [SerializeField] bool canAttack;

    [Header("OTHERS")]
    [SerializeField] Animator animator;


    private void Start()
    {
        attackCollider.enabled = false;
        canAttack = true;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
            StartCoroutine(Attack());
    }


    IEnumerator Attack()
    {
        transform.position = transform.position;
        animator.SetTrigger("isAttacking");
        canAttack = false;

        yield return new WaitForSeconds(1);

        attackCollider.enabled = false;

        yield return new WaitForSeconds(1);

        attackCollider.enabled = true;
        canAttack = true;
    }
}
