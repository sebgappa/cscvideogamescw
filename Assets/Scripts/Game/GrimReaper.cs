using UnityEngine;

public class GrimReaper : MonoBehaviour
{
    void Start()
    {
        Player.OnPlayerDead += KillPlayer;
        Player.OnPlayerRevive += RevivePlayer;
    }

    private void KillPlayer(Player player)
    {
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Animator>().SetBool("isDead", true);
    }

    private void RevivePlayer(Player player)
    {
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Animator>().SetBool("isDead", false);
        player.GetComponent<Animator>().SetTrigger("Revive");
    }
}
