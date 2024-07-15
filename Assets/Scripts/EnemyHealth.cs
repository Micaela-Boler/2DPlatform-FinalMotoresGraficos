using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [Header("DROPS")]
    [SerializeField] GameObject[] drops;
    GameObject randomDrop;
    int randomNumber;

    [Header("ENEMY DEATH")]
    [SerializeField] float timeToDestroyEnemy;
    [SerializeField] Collider2D enemyCollider;



    protected override void TakeDamage()
    {
        characterCanMove = gameObject.GetComponent<Enemy>().canMove;

        base.TakeDamage();

        if (health <= 0)
        {
            enemyCollider.enabled = false;

            animator.SetTrigger("Death");
            EnemyDrop();

            Destroy(gameObject, timeToDestroyEnemy);
        }
    }


    private void EnemyDrop()
    {
        randomNumber = Random.Range(0, drops.Length);
        randomDrop = drops[randomNumber];

        Instantiate(randomDrop, transform.position, Quaternion.identity);
    }
}
