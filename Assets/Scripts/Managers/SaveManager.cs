using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour
{
    [Inject]
    BoardGeneratingManager boardGeneratingManager;

    BoardSaveData saveData = new BoardSaveData();
    const string boardSavePath = "BoardSave";

    public string BoardSavePath { get => boardSavePath; }

    public void SaveBoard(Dictionary<Vector2Int, BoardObject> boardDictionary)
    {
        List<int> numberObjectList = new List<int>();
        foreach(KeyValuePair<Vector2Int , BoardObject> boardObject in boardDictionary)
        {
            numberObjectList.Add(boardObject.Value.NumberObject.Value);
        }

        saveData.savedNumberObjectList = numberObjectList;

        string savePath = Application.persistentDataPath + BoardSavePath;
        SaveGame(savePath, saveData);
    }

    public void LoadBoard(Dictionary<Vector2Int, BoardObject> boardDictionary)
    {
        string savePath = Application.persistentDataPath + BoardSavePath;
        saveData = LoadGame(savePath);
        List<int> numberObjectList = saveData.savedNumberObjectList;

        for (int i = 0; i < numberObjectList.Count; i++)
        {
            boardGeneratingManager.CreateNumberObject(boardDictionary.ElementAt(i).Value, numberObjectList[i]);
        }
    }

    public bool IsBoardSaved()
    {
        string savePath = Application.persistentDataPath + BoardSavePath;

        if (!File.Exists(savePath)) return false;

        return true;
    }

    public void DeleteSaveByInspectorButton()
    {
        string savePath = Application.persistentDataPath + BoardSavePath;

        if (!File.Exists(savePath)) return;

        File.Delete(savePath);
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
}

[Serializable]
public class BoardSaveData
{
    public List<int> savedNumberObjectList = new List<int>();
}
