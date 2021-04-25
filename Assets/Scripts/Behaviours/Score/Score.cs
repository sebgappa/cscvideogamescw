using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
