using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static event Action<Player> OnGameOver;
    public Text scoreText;
    public Text healthText;
    private int score = 0;

    public void Start()
    {
        GetComponent<Player>().OnWinner += UpdateStats;
        GetComponent<Player>().OnDamage += DecreaseHealth;
        Player.OnMatchOver += ResetStats;
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    private void UpdateStats()
    {
        score++;
        if(score == 5)
        {
            OnGameOver.Invoke(GetComponent<Player>());
        }
        scoreText.text = score.ToString();
    }

    private void ResetStats()
    {
        healthText.text = "100";
    }

    private void DecreaseHealth(int health)
    {
        healthText.text = health.ToString();
    }
}
