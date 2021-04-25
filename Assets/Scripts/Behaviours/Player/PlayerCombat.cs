using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    public float attackRate = 2f;

    public bool rangedAttack = false;
    public float bulletForce = 10f;
    public GameObject bulletPrefab;

    private float nextAttackTime = 0;
    private Animator animator;

    public void OnAttack(InputAction.CallbackContext input)
    {
        if(Time.time >= nextAttackTime && input.started)
        {
            if(rangedAttack)
            {
                Shoot();
            } else
            {
                Attack();
            }
            nextAttackTime = Time.time + 1/attackRate;
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Attack");

        var attackPoint = attackPointRight;
        var bulletDirection = attackPointRight.right;

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        
        if(!sr.flipX)
        {
            attackPoint = attackPointLeft;
            bulletDirection = -attackPointLeft.right;
        }

        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        var attackPoint = attackPointRight;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        if (!sr.flipX)
        {
            attackPoint = attackPointLeft;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointRight == null || attackPointLeft == null) return;
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
    }

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
}
