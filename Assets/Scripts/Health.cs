using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [Header("HEALTH AND DAMAGE")]
    [SerializeField] public int health;
    [SerializeField] protected int damage;
    [SerializeField] protected string collisionGameObject;
    [SerializeField] protected string collisionGameObjectB;
    [SerializeField] protected float losingControlTime;
    [HideInInspector] public bool canTakeDamage;

    [Header("PUSH CHARACTER")]
    [SerializeField] Vector2 pushSpeed;
    [SerializeField] protected Rigidbody2D _rb;
    [HideInInspector] public bool characterCanMove;

    [Header("OTHERS")]
    [SerializeField] protected Animator animator;



    protected void Awake()
    {
        canTakeDamage = true;
    }

    protected virtual void TakeDamage()
    {
        health -= damage;
        StartCoroutine(loseControl(characterCanMove));
    }

    protected void Push(Vector2 hitPoint, Rigidbody2D rb)
    {
        rb.velocity = new Vector2(pushSpeed.x * hitPoint.x, pushSpeed.y);
    }

    IEnumerator loseControl(bool canMove)
    {
        animator.SetBool("takingDamage", true);
        canMove = false;

        yield return new WaitForSeconds(losingControlTime);

        animator.SetBool("takingDamage", false);

        yield return new WaitForSeconds(losingControlTime);

        canMove = true;
    }


    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(collisionGameObject) && canTakeDamage)
        {
            TakeDamage();
            Push(collision.GetContact(0).normal, _rb);
        }


        if (collision.gameObject.CompareTag("DeathZone"))
        {
            health = 0;
            TakeDamage();
        }
    }
}
