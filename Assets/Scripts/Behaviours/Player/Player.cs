using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private PlayerInput playerInput;
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    private Score score;

    public void TakeDamage(int damage)
    {
        rigidBody2D.drag = 3;
        animator.SetTrigger("Hurt");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        animator.SetBool("isDead", true);
        boxCollider2D.enabled = false;
        playerInput.enabled = false;
        rigidBody2D.velocity = new Vector2(0, 0);
        score.IncreaseScore();
        yield return new WaitForSeconds(3);
        Revive();
    }

    private void Revive()
    {
        animator.SetBool("isDead", false);
        animator.SetTrigger("Revive");
        currentHealth = maxHealth;
        boxCollider2D.enabled = true;
        playerInput.enabled = true;
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
        score = gameObject.GetComponent<Score>();
    }
}
