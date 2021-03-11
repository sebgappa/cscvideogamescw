using UnityEngine;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;

    public void TakeDamage(int damage)
    {
        Debug.Log("Here!");
        Debug.Log(damage);
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("You fucking died");
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
}
