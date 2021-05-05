using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    public void OnMove(InputAction.CallbackContext context)
    {
        var direction = context.action.ReadValue<Vector2>();

        if (direction.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (direction.x > 0)
        {
            _spriteRenderer.flipX = true;
        }

        var newVelocity = _rigidbody2D.velocity;
        newVelocity.x = direction.x * speed;
        newVelocity.y = direction.y * speed;
        _rigidbody2D.velocity = newVelocity;

        if (direction.x != 0)
        {
            _animator.SetFloat("Speed", Mathf.Abs(newVelocity.x));
        }
        else
        {
            _animator.SetFloat("Speed", Mathf.Abs(newVelocity.y));
        }
    }

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }
}
