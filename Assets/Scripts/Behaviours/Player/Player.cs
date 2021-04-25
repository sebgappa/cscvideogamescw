using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private PlayerInput playerInput;
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;

    public void TakeDamage(int damage)
    {
        rigidBody2D.drag = 3;
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
        boxCollider2D.enabled = false;
        playerInput.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }
}
