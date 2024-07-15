using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("SCREEN")]
    [SerializeField] GameObject screen;

    [Header("GAME MANAGER")]
    public GameManager gameManager;



    private void Start()
    {
        screen.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            gameManager.panelManager(screen);
    }
}
