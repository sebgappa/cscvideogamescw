using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static event Action<Player> OnGameOver;
    public event Action<float> OnStrengthIncrease;
    public event Action<float> OnSpeedIncrease;
    public event Action<float> OnStrengthReset;
    public event Action<float> OnSpeedReset;
    public event Action OnDead;

    public Text scoreText;
    public Text healthText;
    public Text powerUpText;

    private int _score = 0;
    private readonly float _maxHealth = 100;
    private float _currentHealth = 100;
    private int _winningScore = 3;

    public void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Awake()
    {
        GetComponent<Player>().OnWinner += IncreaseScore;
        GetComponent<Player>().OnDamage += DecreaseHealth;
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        healthText.text = "100";
    }

    public void IncreaseHealth(int factor)
    {
        _currentHealth += factor;
        healthText.text = _currentHealth.ToString();
        powerUpText.text = "HEALTH";
        powerUpText.enabled = true;
    }

    public void IncreaseStrength(float factor)
    {
        OnStrengthIncrease.Invoke(factor);
        powerUpText.text = "STRENGTH";
        powerUpText.enabled = true;
    }

    public void ResetStrength(float factor)
    {
        OnStrengthReset(factor);
        powerUpText.enabled = false;
    }

    public void IncreaseSpeed(float factor)
    {
        OnSpeedIncrease.Invoke(factor);
        powerUpText.text = "SPEED";
        powerUpText.enabled = true;
    }

    public void ResetSpeed(float factor)
    {
        OnSpeedReset.Invoke(factor);
        powerUpText.enabled = false;
    }

    public void ResetScore()
    {
        _score = 0;
        scoreText.text = _score.ToString();
    }

    public void DisablePowerUpText()
    {
        powerUpText.enabled = false;
    }

    private void IncreaseScore()
    {
        _score++;
        if (_score == _winningScore)
        {
            OnGameOver.Invoke(GetComponent<Player>());
        }
        scoreText.text = _score.ToString();
    }

    private void DecreaseHealth(float damage)
    {
        _currentHealth -= damage;
        healthText.text = _currentHealth.ToString();

        if (_currentHealth <= 0)
        {
            OnDead.Invoke();
        }
    }
}
