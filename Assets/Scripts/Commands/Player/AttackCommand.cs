using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : ICommand
{
    private IPlayerCombat _entity;
    private Transform _attackPointRight;
    private Transform _attackPointLeft;
    private LayerMask _enemyLayers;

    private float _attackRange = 0.5f;
    private int _attackDamage = 20;
    public AttackCommand(IPlayerCombat entity, Transform attackPointRight, Transform attackPointLeft, LayerMask enemyLayers)
    {
        _entity = entity;
        _attackPointRight = attackPointRight;
        _attackPointLeft = attackPointLeft;
        _enemyLayers = enemyLayers;
    }
    public void Execute()
    {
        _entity.Animator.SetTrigger("Attack");

        var attackPoint = _attackPointRight;

        if (!_entity.SpriteRenderer.flipX)
        {
            attackPoint = _attackPointLeft;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Player>().TakeDamage(_attackDamage);
        }
    }
}
