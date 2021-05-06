using System;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private PlayerStats _playerStats;

    [SerializeField]
    private float strengthMultiplier = 1.3f;
    [SerializeField]
    private float speedMultiplier = 1.5f;
    [SerializeField]
    private int healthBoost = 50;
    public void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    public void ApplyPowerUp(PowerUpType powerUp)
    {
        switch(powerUp)
        {
            case PowerUpType.Strength:
                _playerStats.IncreaseStrength(strengthMultiplier);
                break;
            case PowerUpType.Speed:
                _playerStats.IncreaseSpeed(speedMultiplier);
                break;
            case PowerUpType.Health:
                _playerStats.IncreaseHealth(healthBoost);
                break;
            default:
                break;
        }
    }

    public void ResetPowerUp(PowerUpType powerUp)
    {
        switch (powerUp)
        {
            case PowerUpType.Strength:
                _playerStats.ResetStrength(strengthMultiplier);
                break;
            case PowerUpType.Speed:
                _playerStats.ResetSpeed(speedMultiplier);
                break;
            case PowerUpType.Health:
                _playerStats.disablePowerUpText();
                break;
            default:
                break;
        }
    }
}
