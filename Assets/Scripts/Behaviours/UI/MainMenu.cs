using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private InputField _newProfileName;
    [SerializeField]
    private Text _firstHighestScore;
    [SerializeField]
    private Text _secondHighestScore;
    [SerializeField]
    private Text _thirdHighestScore;
    [SerializeField]
    private Dropdown _playerOneProfiles;
    [SerializeField]
    private Dropdown _playerTwoProfiles;

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

    public void ChangePlayerOneProfile(int val)
    {
        DataPersistance.SetPlayerOneProfile(val);
    }

    public void ChangePlayerTwoProfile(int val)
    {
        DataPersistance.SetPlayerTwoProfile(val);

    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(tutorialIndex);
    }

    public void AddProfile()
    {
        if (!Regex.IsMatch(_newProfileName.text, "^[a-zA-Z]+$")) return;

        DataPersistance.AddProfile(new Profile
        {
            name = _newProfileName.text,
            highscore = 0
        });

        _newProfileName.Select();
        _newProfileName.text = "";

        SetProfileDropdowns();
        SetHighScores();
    }

    public void SetHighScores()
    {
        ProfileCollection profileCollection = DataPersistance.GetProfiles();
        if (profileCollection == null) return;
        var sortedHighScores = profileCollection.profiles.OrderBy(hi => hi.highscore).ToList();
        sortedHighScores.Reverse();

        // Set top three highscores if they are recorded.
        Text[] highScores = { _firstHighestScore, _secondHighestScore, _thirdHighestScore };
        for(int i = 0; i < sortedHighScores.Count && i < 3; i++)
        {
            highScores[i].text = sortedHighScores[i].name + " " + sortedHighScores[i].highscore.ToString();
        }
    }

    public void SetProfileDropdowns()
    {
        _playerOneProfiles.ClearOptions();
        _playerTwoProfiles.ClearOptions();

        _playerOneProfiles.AddOptions(DataPersistance.GetProfileNames());
        _playerTwoProfiles.AddOptions(DataPersistance.GetProfileNames());
    }
}
