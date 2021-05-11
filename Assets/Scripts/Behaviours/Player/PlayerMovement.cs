using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private PlayerStats _playerStats;

    public void OnMove(InputAction.CallbackContext context)
    {
        Move(context.action.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        if (direction.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (direction.x > 0)
        {
            _spriteRenderer.flipX = true;
        }

        var newVelocity = _rigidbody2D.velocity;
        newVelocity.x = direction.x * _speed;
        newVelocity.y = direction.y * _speed;
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

    private void ResetSpeed(float factor)
    {
        _speed /= factor;
    }

    private void IncreaseSpeed(float factor)
    {
        _speed *= factor;
    }

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
        _playerStats = GetComponent<PlayerStats>();

        _playerStats.OnSpeedIncrease += IncreaseSpeed;
        _playerStats.OnSpeedReset += ResetSpeed;
    }
}
