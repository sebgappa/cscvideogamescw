using UnityEngine;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("You fucking died");
        GetComponent<PlayerCombat>().attackDamage = 0;
        GetComponent<PlayerMovement>().speed = 0;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
}
