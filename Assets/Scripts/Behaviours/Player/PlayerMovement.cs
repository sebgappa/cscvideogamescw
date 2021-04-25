using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public Transform attackPoint;

    private Animator animator;
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.action.ReadValue<Vector2>();

        if (direction.x < 0)
        {
            spriteRenderer.flipX = false;
        } else if(direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }

        rigidBody2D.drag = 0;
        var newVelocity = rigidBody2D.velocity;
        newVelocity.x = direction.x * speed;
        newVelocity.y = direction.y * speed;
        rigidBody2D.velocity = newVelocity;

        if(direction.x != 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(newVelocity.x));
        } else
        {
            animator.SetFloat("Speed", Mathf.Abs(newVelocity.y));
        }



    }

    private void Awake()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }
}
