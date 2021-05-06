using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private PlayerStats _playerStats;

    public void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
        _playerStats = GetComponent<PlayerStats>();

        _playerStats.OnSpeedIncrease += IncreaseSpeed;
        _playerStats.OnSpeedReset += ResetSpeed;
    }

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
}
