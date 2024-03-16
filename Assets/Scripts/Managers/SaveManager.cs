using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour
{
    [Inject]
    BoardGeneratingManager boardGeneratingManager;

    BoardSaveData saveData = new BoardSaveData();
    string boardSavePath;

    private void Awake()
    {
        boardSavePath = Application.persistentDataPath + "BoardSave";
    }

    public void SaveBoard(Dictionary<Vector2Int, BoardObject> boardDictionary)
    {
        List<int> numberObjectList = new List<int>();
        foreach(KeyValuePair<Vector2Int , BoardObject> boardObject in boardDictionary)
        {
            numberObjectList.Add(boardObject.Value.NumberObject.Value);
        }

        saveData.savedNumberObjectList = numberObjectList;

        SaveGame(boardSavePath, saveData);
    }

    public void LoadBoard(Dictionary<Vector2Int, BoardObject> boardDictionary)
    {
        saveData = LoadGame(boardSavePath);
        List<int> numberObjectList = saveData.savedNumberObjectList;

        for (int i = 0; i < numberObjectList.Count; i++)
        {
            boardGeneratingManager.CreateNumberObject(boardDictionary.ElementAt(i).Value, numberObjectList[i]);
        }
    }

    public bool IsBoardSaved()
    {
        if (!File.Exists(boardSavePath)) return false;

        return true;
    }

    void SaveGame(string path, BoardSaveData boardSaveData)
    {
        string saveData = JsonUtility.ToJson(boardSaveData);
        File.WriteAllText(path, saveData);
    }

    BoardSaveData LoadGame(string path)
    {
        if (!File.Exists(path))
            return null;
        
        string loadedData = File.ReadAllText(path);
        BoardSaveData boardSaveData = JsonUtility.FromJson<BoardSaveData>(loadedData);
        return boardSaveData;
    }

    void DeleteSave(string path)
    {
        if (!File.Exists(path)) return;

        File.Delete(path);
    }
}

[Serializable]
public class BoardSaveData
{
    public List<int> savedNumberObjectList = new List<int>();
}
