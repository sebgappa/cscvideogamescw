using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandDispatcher))]
public class PlayerCombat : MonoBehaviour, IEntity
{
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public GameObject bulletPrefab;
    public LayerMask enemyLayers;
    public bool rangedAttack = false;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private CommandDispatcher _commandDispatcher;
    private PlayerStats _playerStats;

    [SerializeField]
    private float _bulletDamage = 20;
    [SerializeField]
    private float _damage = 20;
    [SerializeField]
    private float _attackRange = 0.5f;
    [SerializeField]
    private float _attackRate = 2f;
    [SerializeField]
    private float _nextAttackTime = 0;
    [SerializeField]
    private float _bulletForce = 10f;

    public void Awake()
    {
        _commandDispatcher = GetComponent<CommandDispatcher>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerStats = GetComponent<PlayerStats>();

        _playerStats.OnStrengthIncrease += IncreaseStrength;
        _playerStats.OnStrengthReset += ResetStrength;
    }

    public void OnAttack(InputAction.CallbackContext input)
    {
        if(Time.time >= _nextAttackTime && input.started)
        {
            _animator.SetTrigger("Attack");

            if (rangedAttack)
            {
                Shoot();
            } else
            {
                Melee();
            }
            _nextAttackTime = Time.time + 1/_attackRate;
        }
    }

    private void ResetStrength(float factor)
    {
        if (rangedAttack)
        {
            _bulletDamage /= factor;
        }
        else
        {
            _damage /= factor;
        }
    }

    private void IncreaseStrength(float factor)
    {
        if(rangedAttack)
        {
            _bulletDamage *= factor;
        } else
        {
            _damage *= factor;
        }
    }

    private void Melee() 
    {
        var attackPoint = attackPointRight;
        if (!_spriteRenderer.flipX)
        {
            attackPoint = attackPointLeft;
        }

        _commandDispatcher.DispatchCommand(new MeleeCommand(this, attackPoint, enemyLayers, _damage, _attackRange));
    }

    private void Shoot() 
    {
        var attackPoint = attackPointRight;
        var bulletDirection = attackPointRight.right;
        if (!_spriteRenderer.flipX)
        {
            attackPoint = attackPointLeft;
            bulletDirection = -attackPointLeft.right;
        }

        _commandDispatcher.DispatchCommand(new ShootCommand(this, attackPoint, bulletDirection, bulletPrefab, _bulletForce, _bulletDamage));
    }

}
