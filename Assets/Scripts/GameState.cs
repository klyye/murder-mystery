using System;
using System.Collections.Generic;

[Serializable]
public class GameState
{
    public List<Evidence> inventory;
}

public interface ISaveable
{
    void PopulateSaveData(GameState saveData);
    void LoadFromSaveData(GameState saveData);
}