using UnityEngine;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    private Animator animator;

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

        GetComponent<PlayerCombat>().attackDamage = 0;
        GetComponent<PlayerMovement>().speed = 0;
        GetComponent<PlayerMovement>().direction.x = 0;
        GetComponent<BoxCollider2D>().enabled = false;
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
    }
}
