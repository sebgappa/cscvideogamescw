using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandDispatcher))]
public class PlayerCombat : MonoBehaviour, IEntity
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public Transform attackPointRight;
    public Transform attackPointLeft;
    public GameObject bulletPrefab;
    public LayerMask enemyLayers;
    public bool rangedAttack = false;
    
    private CommandDispatcher _commandDispatcher;

    [SerializeField]
    private float attackRate = 2f;
    [SerializeField]
    private float nextAttackTime = 0;

    public void OnAttack(InputAction.CallbackContext input)
    {
        if(Time.time >= nextAttackTime && input.started)
        {
            _animator.SetTrigger("Attack");

            if (rangedAttack)
            {
                Shoot();
            } else
            {
                Melee();
            }
            nextAttackTime = Time.time + 1/attackRate;
        }
    }

    private void Melee() 
    {
        var attackPoint = attackPointRight;
        if (!_spriteRenderer.flipX)
        {
            attackPoint = attackPointLeft;
        }

        _commandDispatcher.DispatchCommand(new MeleeCommand(this, attackPoint, enemyLayers));
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

        _commandDispatcher.DispatchCommand(new ShootCommand(this, attackPoint, bulletDirection, bulletPrefab));
    }

    private void Awake()
    {
        _commandDispatcher = gameObject.GetComponent<CommandDispatcher>();
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
}
