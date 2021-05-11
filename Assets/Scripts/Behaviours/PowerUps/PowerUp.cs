using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PowerUp : MonoBehaviour
{
    public static event Action OnPowerUpCollected;

    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;
    private int _powerUpDuration = 5;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Collect(collision));
        }
    }

    private IEnumerator Collect(Collider2D player)
    {
        OnPowerUpCollected?.Invoke();
        PowerUpManager powerUpManager = player.GetComponent<PowerUpManager>();

        Array powerUps = Enum.GetValues(typeof(PowerUpType));
        var powerUp = (PowerUpType)UnityEngine.Random.Range(0, powerUps.Length);

        powerUpManager.ApplyPowerUp(powerUp);
        _boxCollider2D.enabled = false;
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(_powerUpDuration);
        powerUpManager.ResetPowerUp(powerUp);
        _boxCollider2D.enabled = true;
        _spriteRenderer.enabled = true;
    }
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
