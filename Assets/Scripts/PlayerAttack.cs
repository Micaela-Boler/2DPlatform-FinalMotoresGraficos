using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [Header("ATTACK")]
    [SerializeField] Collider2D attackCollider;
    [SerializeField] GameObject attackGameObject;
    [SerializeField] bool canAttack;

    [Header("SHOOT")]

    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform firingPoint;
    [SerializeField] float attackCooldown;


    [Header("OTHERS")]
    [SerializeField] Animator animator;


    private void Start()
    {
        //attackCollider.enabled = false;
        attackGameObject.SetActive(false);
        canAttack = true;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
            StartCoroutine(isShooting()/*Attack()*/);
    }

    //disparo
    /*
    IEnumerator Attack()
    {

        animator.SetTrigger("isAttacking");
        canAttack = false;
        //nopuede moverse

        yield return new WaitForSeconds(0.3f);

        //attackCollider.enabled = true;
        attackGameObject.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        //attackCollider.enabled = false;
        attackGameObject.SetActive(false);
        canAttack = true;
    }
    */
    IEnumerator isShooting()
    {
        gameObject.GetComponent<MovePlayer>().canMove = false;
        animator.SetTrigger("isAttacking");
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        Shoot();
        gameObject.GetComponent<MovePlayer>().canMove = true;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }


    void Shoot()
    {
        Bullet projectile = Instantiate(bulletPrefab, firingPoint.position, transform.rotation);

        if (gameObject.transform.localScale.x > 0)
        {
            projectile.LaunchBullet(transform.right);
        }
        else
            projectile.LaunchBullet(-transform.right);  
    }
}
