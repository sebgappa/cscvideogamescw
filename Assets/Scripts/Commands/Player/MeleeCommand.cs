using UnityEngine;

public class MeleeCommand : Command
{
    private Transform _attackPoint;
    private Transform _attackPointLeft;
    private LayerMask _enemyLayers;

    private float _attackRange = 0.5f;
    private int _attackDamage = 20;
    public MeleeCommand(IEntity entity, Transform attackPoint, LayerMask enemyLayers) : base(entity)
    {
        _entity = entity;
        _attackPoint = attackPoint;
        _enemyLayers = enemyLayers;
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
