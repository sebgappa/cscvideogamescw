﻿using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public event Action OnWinner;
    public event Action<float> OnDamage;
    public static event Action OnMatchOver;
    public static event Action<Player> OnPlayerDead;
    public static event Action<Player> OnPlayerRevive;
    public static event Action OnMourn;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private Vector3 _startingPosition;

    [SerializeField]
    private string _opponentName;
    [SerializeField]
    private int respawnPeriod = 2;

    public string GetOpponentsName()
    {
        return _opponentName;
    }

    public void TakeDamage(float damage)
    {
        _rigidbody2D.velocity = new Vector2(0, 0);
        _animator.SetTrigger("Hurt");
        
        OnDamage?.Invoke(damage);
    }

    public void ResetPosition()
    {
        transform.position = _startingPosition;
    }

    private void StartDeathCoroutine()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        OnPlayerDead?.Invoke(this);
        OnMourn?.Invoke();
        yield return new WaitForSeconds(respawnPeriod);
        OnPlayerRevive?.Invoke(this);
        OnMatchOver?.Invoke();
        OnWinner?.Invoke();
    }

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        GetComponent<PlayerStats>().OnDead += StartDeathCoroutine;
    }
}
