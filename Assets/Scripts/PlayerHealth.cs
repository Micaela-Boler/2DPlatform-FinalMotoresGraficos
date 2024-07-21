using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public HealthBar healthBar;
    public GameManager manager;

    [Header("SCREEN")]
    [SerializeField] GameObject screen;



    private void Start()
    {
        healthBar.StartHealth(health);
        screen.SetActive(false);
        canTakeDamage = true;
    }

    protected override void TakeDamage()
    {
        characterCanMove = gameObject.GetComponent<MovePlayer>().canMove;

        base.TakeDamage();
        StartCoroutine(Immunity());
        healthBar.ChangeActualHealth(health);

        if (health <= 0)
            StartCoroutine(waitForPanel());
    }


    private IEnumerator waitForPanel()
    {
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(2);

        manager.panelManager(screen);
    }


    IEnumerator Immunity()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(2);

        canTakeDamage = true;
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(collisionGameObject) && canTakeDamage)
        {
            TakeDamage();
            //Push(-transform.position, _rb);
        }

    }
}
