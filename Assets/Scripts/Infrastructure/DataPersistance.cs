using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataPersistance
{

    private static string FILE = "/save.txt";
    private static ProfileCollection _profileCollection;
    private static Profile _playerOne;
    private static Profile _playerTwo;

    static DataPersistance()
    {
        LoadProfiles();
    }

    public static void SetPlayerOneProfile(int index)
    {
        _playerOne = _profileCollection.profiles[index];

    }

    public static void SetPlayerTwoProfile(int index)
    {
        _playerTwo = _profileCollection.profiles[index];
    }

    public static Profile GetPlayerOneProfile()
    {
        return _playerOne;
    }

    public static Profile GetPlayerTwoProfile()
    {
        return _playerTwo;
    }

    public static void SaveProfiles()
    {
        string json = JsonUtility.ToJson(_profileCollection);
        File.WriteAllText(Application.dataPath + FILE, json);
    }

    public static void AddProfile(Profile profile)
    {
        if (_profileCollection == null) _profileCollection = new ProfileCollection();

        _profileCollection.profiles.Add(profile);
        SaveProfiles();
    }

    public static void LoadProfiles()
    {
        if (!File.Exists(Application.dataPath + FILE)) return;
        
        string fileText = File.ReadAllText(Application.dataPath + FILE);
        _profileCollection = JsonUtility.FromJson<ProfileCollection>(fileText);
    }

    public static ProfileCollection GetProfiles()
    {
        return _profileCollection;
    }

    public static List<string> GetProfileNames()
    {
        List<string> profilesNames = new List<string>();

        if (_profileCollection == null) return profilesNames;

        for (int i = 0; i < _profileCollection.profiles.Count; i++)
        {
            profilesNames.Add(_profileCollection.profiles[i].name);
        }

        return profilesNames;
    }
}
