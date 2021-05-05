using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text scoreText;
    public Text healthText;
    private int score = 0;

    public void Start()
    {
        GetComponent<Player>().OnWinner += UpdateStats;
        GetComponent<Player>().OnDamage += DecreaseHealth;
        Player.OnMatchOver += ResetStats;
    }

    private void UpdateStats()
    {
        score++;
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
