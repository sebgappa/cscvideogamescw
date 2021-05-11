using UnityEngine;

public class MeleeCommand : Command
{
    private Transform _attackPoint;
    private Transform _attackPointLeft;
    private LayerMask _enemyLayers;
    private float _attackRange ;
    private float _attackDamage;
    public MeleeCommand(IEntity entity, Transform attackPoint, LayerMask enemyLayers, float attackDamage, float attackRange, float time) : base(entity, time)
    {
        _entity = entity;
        _time = time;
        _attackPoint = attackPoint;
        _enemyLayers = enemyLayers;
        _attackDamage = attackDamage;
        _attackRange = attackRange;
    }
    public override void Dispatch()
    {
        _entity.animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Player>().TakeDamage(_attackDamage);
        }
    }
}
