using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MovePlayer : MonoBehaviour
{
    [Header("RUN")]

    [SerializeField] float speed;
    public Vector3 scale;
    float horizontal;
    Vector2 movement;



    [Header("JUMP")]

    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [HideInInspector] public int availableJumps;


    [Header("ROTATE")]

    bool right;


    [Header("DASH")]

    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;
    bool canDash = true;
    public bool canMove = true;


    [SerializeField] Animator animator;



    void Update()
    {
       if (canMove)
        {
            Running();

            if (horizontal < 0 && !right)
            {
                RotatePlayer();
            }
            else if (horizontal > 0 && right)
            {
                RotatePlayer();
            }

            if (Input.GetKeyDown(KeyCode.W) && availableJumps > 0)
                Jumping();
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
            StartCoroutine(Dash());
    }



    private void Running()
    {
        horizontal = Input.GetAxis("Horizontal");

        movement = new Vector2(horizontal, 0);
        movement.Normalize();

        transform.Translate(movement * Time.deltaTime * speed, Space.World);

        animator.SetFloat("isRunning", Mathf.Abs(horizontal));
    }


    private void RotatePlayer()
    {
        right = !right;

        scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    
    private IEnumerator Dash()
    {
        canMove = false;
        canDash = false;
        availableJumps = 0;
        rb.gravityScale = 0;
        trailRenderer.emitting = true;
        rb.velocity = new Vector2(dashSpeed * transform.localScale.x, 0);

        animator.SetTrigger("Dash");


        yield return new WaitForSeconds(dashTime);


        canMove = true;
        rb.gravityScale = 1;
        trailRenderer.emitting = false;
        rb.velocity = new Vector2(0, 0);


        yield return new WaitForSeconds(dashCooldown);


        canDash = true;
    }



    private void Jumping()
    {
        rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
        availableJumps -= 1;

        animator.SetFloat("RBVelocityY", rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            availableJumps = 2;
    }
}
