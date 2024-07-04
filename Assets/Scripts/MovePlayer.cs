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



    void Update()
    {
        Running();
        Jumping();
        RotatePlayer(); 
    }


    private void Running()
    {
        horizontal = Input.GetAxis("Horizontal");

        movement = new Vector2(horizontal, 0);
        movement.Normalize();

        transform.Translate(movement * Time.deltaTime * speed);
    }


    private void Jumping()
    {

        if (Input.GetKeyDown(KeyCode.W) && availableJumps > 0)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            availableJumps -= 1;
        }

    }


    private void RotatePlayer()
    {
        // si horizontal es -1 escala es -1, si horizontal es 1 entonces la escala tambien
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            availableJumps = 2;
    }


    // girar

    //planear

    // shift dash o reemplazar el salto

    // click izq ataque    rutina
    // click der disparo

}
