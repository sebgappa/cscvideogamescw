using UnityEngine;

public class ShootCommand : Command
{
    private Transform _attackPoint;
    private Vector3 _bulletDirection;
    private GameObject _bullet;
    private float _bulletForce = 10f;

    public ShootCommand(IEntity entity, Transform attackPoint, Vector3 bulletDirection, GameObject bullet) : base(entity)
    {
        _entity = entity;
        _attackPoint = attackPoint;
        _bulletDirection = bulletDirection;
        _bullet = bullet;
    }
    public override void Execute()
    {
        GameObject bullet = Object.Instantiate(_bullet, _attackPoint.position, _attackPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(_bulletDirection * _bulletForce, ForceMode2D.Impulse);
    }
}
