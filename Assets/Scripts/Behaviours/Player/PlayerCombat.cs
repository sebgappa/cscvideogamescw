using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    public float attackRate = 2f;

    private float nextAttackTime = 0;
    private Animator animator;

    public void OnAttack(InputAction.CallbackContext input)
    {
        if(Time.time >= nextAttackTime && input.started)
        {
            Attack();
            nextAttackTime = Time.time + 1/attackRate;
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit!" + enemy.name);
            enemy.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
}
