using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     https://github.com/UnityTechnologies/UniteNow20-Persistent-Data/blob/main/SaveDataManager.cs
/// </summary>
public static class SaveDataManager
{
    public static void SaveJsonData(IEnumerable<ISaveable> saveables)
    {
        var sd = new GameState();
        foreach (var saveable in saveables) saveable.PopulateSaveData(sd);

        if (FileManager.WriteToFile("GameState.dat", JsonUtility.ToJson(sd))) Debug.Log("Save successful");
    }

    public static void LoadJsonData(IEnumerable<ISaveable> saveables)
    {
        if (FileManager.LoadFromFile("GameState.dat", out var json))
        {
            var sd = new GameState();
            JsonUtility.FromJsonOverwrite(json, sd);

            foreach (var saveable in saveables) saveable.LoadFromSaveData(sd);

            Debug.Log("Load complete");
        }
    }
}