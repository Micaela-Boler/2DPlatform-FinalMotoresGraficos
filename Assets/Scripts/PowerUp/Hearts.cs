using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : PowerUp
{
    [SerializeField] int recoveredHealth;
    GameObject healthBar;


    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
    }

    protected override void ApplyPowerUp()
    {
        if (player.GetComponent<PlayerHealth>().health < 85)
        {
            player.GetComponent<PlayerHealth>().health += recoveredHealth;
            healthBar.GetComponent<HealthBar>().ChangeActualHealth(player.GetComponent<PlayerHealth>().health);
        }
    }
}
