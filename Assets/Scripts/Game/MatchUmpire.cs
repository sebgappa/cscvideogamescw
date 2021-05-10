﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MatchUmpire : MonoBehaviour
{
    public GameObject gameOverMenu;
    public Text winningPlayerText;

    private Player[] players;
    private int playerOneLayer = 8;

    public void Start()
    {
        Debug.Log("MatchUmpireStart");
        players = FindObjectsOfType<Player>();
        Player.OnMatchOver += ResetMatch;
        Player.OnMourn += DisablePlayers;
        PlayerStats.OnGameOver += GameOver;
    }

    public void Rematch()
    {
        Time.timeScale = 1f;
        gameOverMenu.SetActive(false);
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerStats>().ResetScore();
        }
    }

    private void ResetMatch()
    {
        Debug.Log(players.Length);
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
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        winningPlayerText.text = loser.OpponentName;

        if (loser.gameObject.layer == playerOneLayer)
        {
            DataPersistance.GetPlayerTwoProfile().highscore += 1;
        }
        else
        {
            DataPersistance.GetPlayerOneProfile().highscore += 1;
        }
        DataPersistance.SaveProfiles();
    }
}
