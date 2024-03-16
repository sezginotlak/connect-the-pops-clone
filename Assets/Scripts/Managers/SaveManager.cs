using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour
{
    [Inject]
    BoardGeneratingManager boardGeneratingManager;

    [SerializeField]
    SaveDataSO saveDataSO;

    public void SaveBoard(Dictionary<Vector2Int, BoardObject> boardDictionary)
    {
        List<int> numberObjectList = new List<int>();
        foreach(KeyValuePair<Vector2Int , BoardObject> boardObject in boardDictionary)
        {
            numberObjectList.Add(boardObject.Value.NumberObject.Value);
        }

        saveDataSO.SaveData(numberObjectList);
    }

    public void LoadBoard(Dictionary<Vector2Int, BoardObject> boardDictionary)
    {
        List<int> numberObjectList = saveDataSO.LoadData();
        
        for(int i = 0; i < numberObjectList.Count; i++)
        {
            boardGeneratingManager.CreateNumberObject(boardDictionary.ElementAt(i).Value, numberObjectList[i]);
        }

    }

    public bool IsBoardSaved()
    {
        return saveDataSO.IsSaved();
    }
}
