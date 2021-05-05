using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnWinner;
    public event Action<int> OnDamage;
    public static event Action OnMatchOver;
    public static event Action<Player> OnPlayerDead;
    public static event Action<Player> OnPlayerRevive;
    public static event Action OnMourn;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private readonly int _maxHealth = 100;
    private int _currentHealth = 100;
    private Vector3 _startingPosition;

    public void TakeDamage(int damage)
    {
        _rigidbody2D.velocity = new Vector2(0, 0);
        _animator.SetTrigger("Hurt");
        _currentHealth -= damage;
        OnDamage?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void ResetPosition()
    {
        transform.position = _startingPosition;
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    private IEnumerator Die()
    {
        OnPlayerDead?.Invoke(this);
        OnMourn?.Invoke();
        yield return new WaitForSeconds(2);
        OnWinner?.Invoke();
        OnPlayerRevive?.Invoke(this);
        OnMatchOver?.Invoke();
    }

    void Start()
    {
        _currentHealth = _maxHealth;
        _startingPosition = transform.position;
    }

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
}
