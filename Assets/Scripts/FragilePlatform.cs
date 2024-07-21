using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] float timeToFall;



    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;
    }


    IEnumerator gravity()
    {
        animator.SetTrigger("Warning");

        yield return new WaitForSeconds(timeToFall);

        rb.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(gravity());
    }

}
