using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform player;
    
    [Header("PATROL")]
    [SerializeField] protected Transform[] movementPoints;
    [SerializeField] protected float minDistance;
    [HideInInspector] protected int randomNumber = 0;
    [SerializeField] protected float distanceToAttack;
    [SerializeField] protected float speed;
    public bool canMove;

    [Header("ATTACK")]
    [SerializeField] protected Collider2D attackCollider;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected bool canAttack;

    [Header("OTHERS")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator animator;

    [Header("SPIN")]
    protected Vector3 scale;



    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // PATROL //
        
        canMove = true;

        // ATTACK //

        attackCollider.enabled = false;
        canAttack = true;
    }


    protected void Update()
    {
        EnemyPatrol();
    }
    


    protected virtual void EnemyPatrol()
    {
        if (Vector2.Distance(player.position, transform.position) > distanceToAttack && canMove)
        {
             transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) <= minDistance)
            {
                randomNumber = Random.Range(0, movementPoints.Length);
                Spin(movementPoints[randomNumber], 4);
            }

            //animator.SetBool("isRunning", true);

        }
        else if (Vector2.Distance(player.position, transform.position) < distanceToAttack && canAttack && canMove && gameObject.GetComponent<EnemyHealth>().health > 0)
        {
            //animator.SetBool("isRunning", false);
            StartCoroutine(EnemyAttack(attackCollider, attackSpeed, attackCooldown, "isAttacking"));
        }
            
    }


    protected virtual IEnumerator EnemyAttack(Collider2D usingAttackCollider, float attackTime, float attackCooldown, string animationName)
    {
        transform.position = transform.position;
        animator.SetTrigger(animationName);
        canAttack = false;

        yield return new WaitForSeconds(attackTime);

        usingAttackCollider.enabled = false;

        yield return new WaitForSeconds(attackCooldown);

        usingAttackCollider.enabled = true;
        canAttack = true;
    }

    protected virtual void Spin(Transform target, float scaleValue)
    {
        scale = transform.localScale;

        if (transform.position.x > target.position.x)
        {
            scale.x *= -1;
        }
        else
        {
            scale.x = scaleValue;
        }

        transform.localScale = scale;
    }


}
