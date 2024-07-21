using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : Enemy
{
    [Header("ENEMY SHOOT")]
    
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform firingPoint;



    protected override IEnumerator EnemyAttack(Collider2D usingAttackCollider, float attackTime, float attackCooldown, string animationName)
    {
        transform.position = transform.position;
        animator.SetTrigger("warning");
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown /*1*/);

        animator.SetTrigger("isAttacking");

        yield return new WaitForSeconds(attackSpeed);

        EnemyShoot();

        yield return new WaitForSeconds(attackSpeed /*0.8*/);

        canAttack = true;
    }


    private void EnemyShoot()
    {
        Vector2 targetDirection = player.position - transform.position;

        Bullet projectile = Instantiate(bulletPrefab, firingPoint.position, transform.rotation);
        projectile.LaunchBullet(targetDirection);
    }
}
