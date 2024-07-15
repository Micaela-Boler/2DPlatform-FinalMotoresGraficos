using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : PowerUp
{
    [SerializeField] int pointValue;
    GameObject gameManager;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }


    protected override void ApplyPowerUp()
    {
        gameManager.GetComponent<GameManager>().UpdateScore(pointValue);
    }

}
