using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth :Health
{
    public HealthBar healthBar;



    private void Start()
    {
        healthBar.StartHealth(health);
    }

    protected override void TakeDamage()
    {
        characterCanMove = gameObject.GetComponent<MovePlayer>().canMove;

        base.TakeDamage();
        healthBar.ChangeActualHealth(health);

        if (health <= 0)
        {
            Debug.Log("Perdiste");
            //implementar ui
        }
    }
}
