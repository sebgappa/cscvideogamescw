using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject block;
    public GameObject blade;
    public GameObject powerUp;
    public Text playerOneScore;
    public Text playerTwoScore;
    public Text playerOneHealth;
    public Text playerTwoHealth;
    public Text tutorialText;

    public InputActionReference playerOneMove;
    public InputActionReference playerTwoMove;

    public InputActionReference playerOneAttack;
    public InputActionReference playerTwoAttack;

    [SerializeField]
    private int textDelay;
    private bool powerUpCollected = false;

    private IEnumerator Introduction()
    {
        yield return new WaitForSeconds(textDelay);
        tutorialText.enabled = true;
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "This is your player.";
        playerOne.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "This is your opponents player.";
        playerTwo.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(Player1Movement());
    }

    private IEnumerator Player1Movement()
    {
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "Player one can move using the WASD keys, give it a go.";
        playerOne.GetComponent<PlayerInput>().enabled = true;
        yield return new WaitWhile(() => !playerOne.GetComponent<PlayerInput>().actions.FindAction(playerOneMove.name).triggered);
        tutorialText.text = "Easy as that.";
        StartCoroutine(Player2Movement());
    }

    private IEnumerator Player2Movement()
    {
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "Player two can move using the arrow keys, give it a go.";
        playerTwo.GetComponent<PlayerInput>().enabled = true;
        yield return new WaitWhile(() => !playerTwo.GetComponent<PlayerInput>().actions.FindAction(playerTwoMove.name).triggered);
        tutorialText.text = "Simple.";
        StartCoroutine(Player1Attack());
    }

    private IEnumerator Player1Attack()
    {
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "Player one can attack using the spacebar, give it a go.";
        yield return new WaitWhile(() => !playerOne.GetComponent<PlayerInput>().actions.FindAction(playerOneAttack.name).triggered);
        tutorialText.text = "Nice.";
        StartCoroutine(Player2Attack());
    }

    private IEnumerator Player2Attack()
    {
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "Player two can attack using the enter key on the numpad, give it a go.";
        yield return new WaitWhile(() => !playerTwo.GetComponent<PlayerInput>().actions.FindAction(playerTwoAttack.name).triggered);
        tutorialText.text = "Good.";
        StartCoroutine(StaticObjects());
    }

    private IEnumerator StaticObjects()
    {
        yield return new WaitForSeconds(textDelay);
        block.SetActive(true);
        blade.SetActive(true);
        powerUp.SetActive(true);
        tutorialText.text = "These are the various objects you may encounter during your match.";
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "The spinny blade deals damage.";
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "The block is...";
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "A block.";
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "You can collect strawberries for powerups. Try picking up this one.";
        yield return new WaitWhile(() => (!powerUpCollected));
        tutorialText.text = "Power ups can increase attack, speed, or health, for a period of time.";
        StartCoroutine(Stats());
    }

    private IEnumerator Stats()
    {
        yield return new WaitForSeconds(textDelay);
        playerOneHealth.enabled = true;
        playerTwoHealth.enabled = true;
        playerOneScore.enabled = true;
        playerTwoScore.enabled = true;
        tutorialText.text = "These are your stats.";
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "The coloured numbers represent the score.";
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "The green numbers are your health.";
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "After a player dies, the match is reset.";
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "First to five wins.";
        yield return new WaitForSeconds(textDelay);
        tutorialText.text = "That's the game...fight!";
    }

    private void Awake()
    {
        StartCoroutine(Introduction());
        PowerUp.OnPowerUpCollected += PowerUpCollected;
    }

    private void PowerUpCollected()
    {
        powerUpCollected = true;
    }
}
