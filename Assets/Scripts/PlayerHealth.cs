using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth :Health
{
    public HealthBar healthBar;
    public GameManager manager;

    [Header("SCREEN")]
    [SerializeField] GameObject screen;



    private void Start()
    {
        healthBar.StartHealth(health);
        screen.SetActive(false);
    }

    protected override void TakeDamage()
    {
        characterCanMove = gameObject.GetComponent<MovePlayer>().canMove;

        base.TakeDamage();
        healthBar.ChangeActualHealth(health);

        if (health <= 0)
        {
            //animacion de muerte
            manager.panelManager(screen);
        }
    }
}
