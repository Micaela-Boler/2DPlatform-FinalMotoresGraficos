using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [Header("RUN")]
    
    [SerializeField] float speed;

    private float horizontal;

    private Vector2 movement;


    [Header("JUMP")]

    [SerializeField] Rigidbody2D rb;

    [SerializeField] float jumpForce;

    [SerializeField] int availableJumps;


    [Header("ROTATE")]

    [SerializeField] bool right;



    void Update()
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


    private void Running()
    {
        horizontal = Input.GetAxis("Horizontal");

        movement = new Vector2(horizontal, 0);
        movement.Normalize();

        transform.Translate(movement * Time.deltaTime * speed, Space.World);
    }


    private void RotatePlayer()
    {
        right = !right;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
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



    //planear

    // barra dash o reemplazar el salto

    // click izq ataque    rutina
    // click der disparo
}
