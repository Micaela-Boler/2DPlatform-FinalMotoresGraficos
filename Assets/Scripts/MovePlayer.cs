using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MovePlayer : MonoBehaviour
{
    [Header("RUN")]
    
    [SerializeField] float speed;
    float horizontal;
    Vector2 movement;


    [Header("JUMP")]

    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [HideInInspector] public int availableJumps;


    [Header("ROTATE")]

    bool right;


    [Header("DASH")]

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

        animator.SetFloat("isRunning", horizontal);
    }


    private void RotatePlayer()
    {
        right = !right;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }



    private IEnumerator Dash()
    {
        canMove = false;
        canDash = false;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(dashSpeed * transform.localScale.x, 0);

        animator.SetTrigger("Dash");


        yield return new WaitForSeconds(dashTime);


        canMove = true;
        rb.gravityScale = 1.5f;


        yield return new WaitForSeconds(dashCooldown);


        canDash = true;
    }



    private void Jumping()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        availableJumps -= 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            availableJumps = 2;
    }
}
