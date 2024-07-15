using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    
    [Header("PATROL")]
    [SerializeField] protected Transform[] movementPoints;
    [SerializeField] protected float distaciaMinima;
    [HideInInspector] protected int randomNumber = 0;
    [SerializeField] protected float rangoDeDeteccion;
    [SerializeField] float speed;
    public bool canMove;

    [Header("ATTACK")]
    [SerializeField] Collider2D attackCollider;
    [SerializeField] bool canAttack;

    [Header("OTHERS")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;





    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // PATROL //
        
        canMove = true;

        // ATTACK //

        attackCollider.enabled = false;
        canAttack = true;
    }


    /*
    protected virtual void Perseguir()
    {
        if (Vector2.Distance(jugador.position, transform.position) < rangoDeDeteccion && jugador != null)
            transform.position = jugador.position;
    }
    */

    private void Update()
    {
        EnemyPatrol();
    }
    


    void EnemyPatrol()
    {
        if (Vector2.Distance(player.position, transform.position) > rangoDeDeteccion && canMove)
        {
             transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) <= distaciaMinima)
            {
                randomNumber = Random.Range(0, movementPoints.Length);
                Spin();
            } 
        }
        else if (Vector2.Distance(player.position, transform.position) < rangoDeDeteccion && canAttack && canMove && gameObject.GetComponent<EnemyHealth>().health > 0)
            StartCoroutine(EnemyAttack());
    }


    protected virtual IEnumerator EnemyAttack()
    {
        transform.position = transform.position;
        animator.SetTrigger("isAttacking");
        canAttack = false;

        yield return new WaitForSeconds(3);

        attackCollider.enabled = false;

        yield return new WaitForSeconds(0.5f);

        attackCollider.enabled = true;
        canAttack = true;
    }

    void Spin()
    {
        if (transform.position.x < movementPoints[randomNumber].position.x)
            spriteRenderer.flipX = false;
        else 
            spriteRenderer.flipX = true;
    }


}
