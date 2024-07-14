using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int damage;

    [SerializeField] protected string collisionGameObject;

    [SerializeField] protected Animator animator;


    [SerializeField] Vector2 pushSpeed;
    [SerializeField] protected Rigidbody2D _rb;
    protected bool characterCanMove;

    protected virtual void TakeDamage()
    {
        health -= damage;
        StartCoroutine(loseControl(characterCanMove));
        
        // empuje
        //drop
        //animacion de muerte
        
    }

    protected void Push(Vector2 hitPoint, Rigidbody2D rb)
    {
        rb.velocity = new Vector2(-pushSpeed.x * hitPoint.x, pushSpeed.y);
    }

    IEnumerator loseControl(bool canMove)
    {
        animator.SetBool("takingDamage", true);
        canMove = false;

        yield return new WaitForSeconds(0.2f);

        animator.SetBool("takingDamage", false);
        canMove = true;
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(collisionGameObject))
        {
            TakeDamage();

            Vector2 gameObjectPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Vector2 collisionPoint = collision.ClosestPoint(transform.position);
            Vector2 pushDirection = gameObjectPosition - collisionPoint;

            Push(pushDirection, _rb);
        }
    }
}
