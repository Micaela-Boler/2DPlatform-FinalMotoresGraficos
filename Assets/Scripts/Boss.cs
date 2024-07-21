using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("BOSS")]
    [SerializeField] Collider2D secondAttackCollider;
    [SerializeField] float actionCooldown;
    [SerializeField] float shieldDuration;


    [Header("ENEMY STATE")]
    public EnemyState enemyState;
    public enum EnemyState
    {
        SecondAttack,
        Chasing,
        Shield,
        Attack
    }



    private void Start()
    {
        enemyState = EnemyState.Chasing;
        secondAttackCollider.enabled = false;
    }



    protected void Update()
    {
        switch (enemyState)
        {

            case EnemyState.Chasing:
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
                break;


            case EnemyState.Shield:
                {
                    Shield();
                }
                break;


            case EnemyState.Attack:
                {
                    base.EnemyAttack(attackCollider, attackSpeed, attackCooldown, "isAttacking");
                }
                break;


            case EnemyState.SecondAttack:
                {
                    base.EnemyAttack(secondAttackCollider, attackSpeed, attackCooldown, "isAttacking2");
                }
                break;
        }



        //CHANGING ENEMY STATE

        if (Vector2.Distance(player.position, transform.position) > distanceToAttack)
        {
            animator.SetBool("isRunning", true);
            enemyState = EnemyState.Chasing;
            Spin(player, 5);
        }
        else 
            ActionCooldown();
    }



    IEnumerator Shield()
    {
        gameObject.GetComponent<EnemyHealth>().canTakeDamage = false;
        transform.position = transform.position;
        animator.SetTrigger("usingShield");

        yield return new WaitForSeconds(shieldDuration);

        gameObject.GetComponent<EnemyHealth>().canTakeDamage = true;
    }


    IEnumerator ActionCooldown()
    {
        animator.SetBool("isRunning", false);

        yield return new WaitForSeconds(actionCooldown);


        if (canAttack && canMove && gameObject.GetComponent<EnemyHealth>().health > 0)
        {
            int randomAction = Random.Range(0, 2);


            switch (randomAction)
            {
                case 0: enemyState = EnemyState.Attack;
                    break;

                case 1: enemyState = EnemyState.Shield;
                    break;

                case 2: enemyState = EnemyState.SecondAttack;
                    break;
            }
        }
    }
}
