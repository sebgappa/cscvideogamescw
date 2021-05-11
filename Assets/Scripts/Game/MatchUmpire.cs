using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MatchUmpire : MonoBehaviour
{
    public GameObject gameOverMenu;
    public Text winningPlayerText;

    private int _playerOneLayer = 8;

    public void Start()
    {
        Player.OnMatchOver += ResetMatch;
        Player.OnMourn += DisablePlayers;
        PlayerStats.OnGameOver += GameOver;
    }

    public void Rematch()
    {
        Time.timeScale = 1f;
        gameOverMenu.SetActive(false);

        var players = FindObjectsOfType<Player>();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerStats>().ResetScore();
        }
    }

    private void ResetMatch()
    {
        var players = FindObjectsOfType<Player>();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].ResetPosition();
            players[i].GetComponent<PlayerStats>().ResetHealth();
            players[i].GetComponent<PlayerInput>().enabled = true;
        }
        
    }

    private void DisablePlayers()
    {
        var players = FindObjectsOfType<Player>();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerInput>().enabled = false;
        }
    }

    private void GameOver(Player loser)
    {
        GetComponent<ReplayService>().StopRecording();

        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        winningPlayerText.text = loser.GetOpponentsName();

        if (loser.gameObject.layer == _playerOneLayer)
        {
            DataPersistance.GetPlayerTwoProfile().highscore += 1;
        }
        else
        {
            DataPersistance.GetPlayerOneProfile().highscore += 1;
        }
        DataPersistance.SaveGameProperties();
    }
}
