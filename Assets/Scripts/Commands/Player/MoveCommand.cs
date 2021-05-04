using UnityEngine;

public class MoveCommand : ICommand
{
    private IPlayerMovement _entity;
    private Vector2 _direction;
    private float speed = 3f;
    public MoveCommand(IPlayerMovement entity, Vector2 direction)
    {
        _entity = entity;
        _direction = direction;
    }
    public void Execute()
    {
        if (_direction.x < 0)
        {
            _entity.SpriteRenderer.flipX = false;
        }
        else if (_direction.x > 0)
        {
            _entity.SpriteRenderer.flipX = true;
        }

        var newVelocity = _entity.Rigidbody2D.velocity;
        newVelocity.x = _direction.x * speed;
        newVelocity.y = _direction.y * speed;
        _entity.Rigidbody2D.velocity = newVelocity;

        if (_direction.x != 0)
        {
            _entity.Animator.SetFloat("Speed", Mathf.Abs(newVelocity.x));
        }
        else
        {
            _entity.Animator.SetFloat("Speed", Mathf.Abs(newVelocity.y));
        }
    }
}
