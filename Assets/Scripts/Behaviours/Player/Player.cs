using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandDispatcher))]
public class Player : MonoBehaviour, IPlayer
{
    public Rigidbody2D Rigidbody2D { get; set; }
    public BoxCollider2D BoxCollider2D { get; set; }
    public PlayerInput PlayerInput { get; set; }
    public Score Score { get; set; }
    public int MaxHealth { get => _maxHealth; }
    public int CurrentHealth { get; set; }
    public Animator Animator { get; set; }

    private readonly int _maxHealth = 100;
    private CommandDispatcher _commandDispatcher;

    public void TakeDamage(int damage)
    {
        _commandDispatcher.DispatchCommand(new DamageCommand(this, damage));

        if (CurrentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        _commandDispatcher.DispatchCommand(new DieCommand(this));
        yield return new WaitForSeconds(3);
        _commandDispatcher.DispatchCommand(new ReviveCommand(this));
    }

    void Start()
    {
        CurrentHealth = _maxHealth;
    }

    private void Awake()
    {
        _commandDispatcher = gameObject.GetComponent<CommandDispatcher>();
        Animator = gameObject.GetComponent<Animator>();
        PlayerInput = gameObject.GetComponent<PlayerInput>();
        Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        BoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        Score = gameObject.GetComponent<Score>();
    }
}
