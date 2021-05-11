using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PowerUpManager : MonoBehaviour
{
    private PlayerStats _playerStats;

    [SerializeField]
    private float _strengthMultiplier = 1.3f;
    [SerializeField]
    private float _speedMultiplier = 1.5f;
    [SerializeField]
    private int _healthBoost = 50;

    public void ApplyPowerUp(PowerUpType powerUp)
    {
        switch(powerUp)
        {
            case PowerUpType.Strength:
                _playerStats.IncreaseStrength(_strengthMultiplier);
                break;
            case PowerUpType.Speed:
                _playerStats.IncreaseSpeed(_speedMultiplier);
                break;
            case PowerUpType.Health:
                _playerStats.IncreaseHealth(_healthBoost);
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
                _playerStats.ResetStrength(_strengthMultiplier);
                break;
            case PowerUpType.Speed:
                _playerStats.ResetSpeed(_speedMultiplier);
                break;
            case PowerUpType.Health:
                _playerStats.DisablePowerUpText();
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
    }
}
