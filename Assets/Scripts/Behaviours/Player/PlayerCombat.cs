using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandDispatcher))]
public class PlayerCombat : MonoBehaviour, IPlayerCombat
{
    public Animator Animator { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public GameObject bulletPrefab;
    public LayerMask enemyLayers;
    public bool rangedAttack = false;
    
    private CommandDispatcher _commandDispatcher;
    private float attackRate = 2f;
    private float nextAttackTime = 0;

    public void OnAttack(InputAction.CallbackContext input)
    {
        if(Time.time >= nextAttackTime && input.started)
        {
            if(rangedAttack)
            {
                _commandDispatcher.DispatchCommand(new ShootCommand(this, attackPointRight, attackPointLeft, bulletPrefab));
            } else
            {
                _commandDispatcher.DispatchCommand(new AttackCommand(this, attackPointRight, attackPointLeft, enemyLayers));
            }
            nextAttackTime = Time.time + 1/attackRate;
        }
    }

    private void Awake()
    {
        _commandDispatcher = gameObject.GetComponent<CommandDispatcher>();
        Animator = gameObject.GetComponent<Animator>();
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
}
