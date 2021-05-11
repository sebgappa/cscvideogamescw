using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataPersistance
{

    private static string FILE = "/save.txt";
    private static GameProperties _gameProperties;
    private static Profile _playerOne;
    private static Profile _playerTwo;

    static DataPersistance()
    {
        LoadProfiles();
    }

    public static void SetPlayerOneProfile(int index)
    {
        _playerOne = _gameProperties.profiles[index];

    }

    public static void SetPlayerTwoProfile(int index)
    {
        _playerTwo = _gameProperties.profiles[index];
    }

    public static Profile GetPlayerOneProfile()
    {
        return _playerOne;
    }

    public static Profile GetPlayerTwoProfile()
    {
        return _playerTwo;
    }

    public static void SaveGameProperties()
    {
        string json = JsonUtility.ToJson(_gameProperties);
        File.WriteAllText(Application.dataPath + FILE, json);
    }

    public static void SetGameVolume(float volume)
    {
        if (_gameProperties == null) _gameProperties = new GameProperties();

        _gameProperties.gameVolume = volume;
        SaveGameProperties();
    }

    public static void AddProfile(Profile profile)
    {
        if (_gameProperties == null) _gameProperties = new GameProperties();

        _gameProperties.profiles.Add(profile);
        SaveGameProperties();
    }

    public static void LoadProfiles()
    {
        if (!File.Exists(Application.dataPath + FILE)) return;

        string fileText = File.ReadAllText(Application.dataPath + FILE);
        _gameProperties = JsonUtility.FromJson<GameProperties>(fileText);
    }

    public static GameProperties GetGameProperties()
    {
        return _gameProperties;
    }

    public static List<string> GetProfileNames()
    {
        List<string> profilesNames = new List<string>();

        if (_gameProperties == null) return profilesNames;

        for (int i = 0; i < _gameProperties.profiles.Count; i++)
        {
            profilesNames.Add(_gameProperties.profiles[i].name);
        }

        return profilesNames;
    }
}
