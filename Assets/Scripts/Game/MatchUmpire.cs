using UnityEngine;
using UnityEngine.InputSystem;

public class MatchUmpire : MonoBehaviour
{
    private Player[] players;

    public void Start()
    {
        players = FindObjectsOfType<Player>();
        Player.OnMatchOver += ResetMatch;
        Player.OnMourn += DisablePlayers;
    }

    private void ResetMatch()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].ResetPosition();
            players[i].ResetHealth();
            players[i].GetComponent<PlayerInput>().enabled = true;
        }
        
    }

    private void DisablePlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerInput>().enabled = false;
        }
    }
}
