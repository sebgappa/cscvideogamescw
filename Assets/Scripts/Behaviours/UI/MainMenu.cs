using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int gameModeToLoad = 1;
    public void PlayGame()
    {
        SceneManager.LoadScene(gameModeToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeGamemode(int val)
    {
        gameModeToLoad = val + 1;
    }
}
