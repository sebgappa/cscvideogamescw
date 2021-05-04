using UnityEngine;

public class DamageCommand : ICommand
{
    private IPlayer _entity;
    private int _damage;
    public DamageCommand(IPlayer entity, int damage)
    {
        _entity = entity;
        _damage = damage;
    }
    public void Execute()
    {
        _entity.Rigidbody2D.velocity = new Vector2(0, 0);
        _entity.Animator.SetTrigger("Hurt");
        _entity.CurrentHealth -= _damage;
    }
}
