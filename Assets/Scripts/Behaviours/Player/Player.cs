using UnityEngine;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private PlayerCombat playerCombat;
    private PlayerMovement playerMovement;
    private BoxCollider2D boxCollider2D;

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        Debug.Log("You fucking died");

        playerCombat.attackDamage = 0;
        playerMovement.speed = 0;
        playerMovement.direction.x = 0;
        boxCollider2D.enabled = false;
        enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }
}
