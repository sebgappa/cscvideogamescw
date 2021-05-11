using UnityEngine;

public class ShootCommand : Command
{
    private Transform _attackPoint;
    private Vector3 _bulletDirection;
    private GameObject _bullet;
    private float _bulletForce;
    private float _bulletDamage;

    public ShootCommand(IEntity entity, Transform attackPoint, Vector3 bulletDirection, GameObject bullet,
                        float bulletForce, float bulletDamage, float time) : base(entity, time)
    {
        _time = time;
        _entity = entity;
        _attackPoint = attackPoint;
        _bulletDirection = bulletDirection;
        _bullet = bullet;
        _bulletForce = bulletForce;
        _bulletDamage = bulletDamage;
    }
    public override void Dispatch()
    {
        _entity.animator.SetTrigger("Attack");

        GameObject bullet = Object.Instantiate(_bullet, _attackPoint.position, _attackPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Bullet>().setDamage(_bulletDamage);
        rb.AddForce(_bulletDirection * _bulletForce, ForceMode2D.Impulse);
    }
}
