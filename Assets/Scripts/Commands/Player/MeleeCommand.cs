using UnityEngine;

public class MeleeCommand : Command
{
    private Transform _attackPoint;
    private Transform _attackPointLeft;
    private LayerMask _enemyLayers;
    private float _attackRange ;
    private float _attackDamage;
    public MeleeCommand(IEntity entity, Transform attackPoint, LayerMask enemyLayers, float attackDamage, float attackRange) : base(entity)
    {
        _entity = entity;
        _attackPoint = attackPoint;
        _enemyLayers = enemyLayers;
        _attackDamage = attackDamage;
        _attackRange = attackRange;
    }
    public override void Execute()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Player>().TakeDamage(_attackDamage);
        }
    }
}
