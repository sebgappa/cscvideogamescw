using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField]
    private AudioMixer _audioMixer;
    [SerializeField]
    private Slider _volumeSlider;

    private int _gameModeToLoad = 1;
    private int _tutorialIndex = 3;

    private int _profile1DropdownSelection = -1;
    private int _profile2DropdownSelection = -1;

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("MasterVolume", volume);
        DataPersistance.SetGameVolume(volume);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(_gameModeToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeGamemode(int val)
    {
        _gameModeToLoad = val + 1;
    }

    public void ChangePlayerOneProfile(int val)
    {
        DataPersistance.SetPlayerOneProfile(val);
        _profile1DropdownSelection = val;
    }

    public void ChangePlayerTwoProfile(int val)
    {
        DataPersistance.SetPlayerTwoProfile(val);
        _profile2DropdownSelection = val;
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(_tutorialIndex);
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
        GameProperties gameProperties = DataPersistance.GetGameProperties();
        if (gameProperties == null) return;
        var sortedHighScores = gameProperties.profiles.OrderBy(hi => hi.highscore).ToList();
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

        if(_profile1DropdownSelection != -1)
        {
            _playerOneProfiles.value = _profile1DropdownSelection;
        }

        if (_profile2DropdownSelection != -1)
        {
            _playerTwoProfiles.value = _profile2DropdownSelection;
        }
    }

    public void SetVolumeSlider()
    {
        var savedVolume = DataPersistance.GetGameProperties().gameVolume;
        _volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    private void Start()
    {
        DataPersistance.SetPlayerOneProfile(0);
        DataPersistance.SetPlayerTwoProfile(0);
        SetVolumeSlider();
    }
}
