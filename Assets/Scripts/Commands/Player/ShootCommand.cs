using UnityEngine;

public class ShootCommand : ICommand
{
    private IPlayerCombat _entity;
    private Transform _attackPointRight;
    private Transform _attackPointLeft;
    private GameObject _bullet;
    private float _bulletForce = 10f;

    public ShootCommand(IPlayerCombat entity, Transform attackPointRight, Transform attackPointLeft, GameObject bullet)
    {
        _entity = entity;
        _attackPointRight = attackPointRight;
        _attackPointLeft = attackPointLeft;
        _bullet = bullet;
    }
    public void Execute()
    {
        _entity.Animator.SetTrigger("Attack");

        var attackPoint = _attackPointRight;
        var bulletDirection = _attackPointRight.right;

        if (!_entity.SpriteRenderer.flipX)
        {
            attackPoint = _attackPointLeft;
            bulletDirection = -_attackPointLeft.right;
        }

        GameObject bullet = Object.Instantiate(_bullet, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletDirection * _bulletForce, ForceMode2D.Impulse);
    }
}
