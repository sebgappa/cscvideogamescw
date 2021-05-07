using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text _profileName;
    [SerializeField]
    private Text _firstHighestScore;
    [SerializeField]
    private Text _secondHighestScore;
    [SerializeField]
    private Text _thirdHighestScore;

    private int gameModeToLoad = 1;
    private int tutorialIndex = 3;
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

    public void LoadTutorial()
    {
        SceneManager.LoadScene(tutorialIndex);
    }

    public void AddProfile()
    {
        if (!Regex.IsMatch(_profileName.text, "[a-zA-Z]")) return;

        DataPersistance.LoadProfiles();
        DataPersistance.SaveProfile(new Profile
        {
            name = _profileName.text,
            highscore = 0
        });
    }

    public void SetHighScores()
    {
        ProfileCollection profileCollection = DataPersistance.GetProfiles();
        var sortedHighScores = profileCollection.profiles.OrderBy(hi => hi.highscore).ToList();
        sortedHighScores.Reverse();

        Text[] highScores = { _firstHighestScore, _secondHighestScore, _thirdHighestScore };
        for(int i = 0; i < sortedHighScores.Count && i < 3; i++)
        {
            highScores[i].text = sortedHighScores[i].name + " " + sortedHighScores[i].highscore.ToString();
        }
    }
}
