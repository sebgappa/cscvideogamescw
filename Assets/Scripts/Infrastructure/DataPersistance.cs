using System.IO;
using UnityEngine;

public static class DataPersistance
{

    private static string FILE = "/save.txt";
    private static ProfileCollection _profileCollection;

    public static void SaveProfile(Profile profile)
    {
        _profileCollection.profiles.Add(profile);
        string json = JsonUtility.ToJson(_profileCollection);
        File.WriteAllText(Application.dataPath + FILE, json);
    }

    public static void LoadProfiles()
    {
        if (!File.Exists(Application.dataPath + FILE)) return;
        
        string fileText = File.ReadAllText(Application.dataPath + FILE);
        _profileCollection = JsonUtility.FromJson<ProfileCollection>(fileText);
    }

    public static ProfileCollection GetProfiles()
    {
        LoadProfiles();
        return _profileCollection;
    }
}
