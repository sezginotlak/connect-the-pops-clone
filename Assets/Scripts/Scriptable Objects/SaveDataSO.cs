using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SaveDatasSO", order = 1)]
public class SaveDataSO : ScriptableObject
{
    public List<int> savedNumberObjectList = new List<int>();
    bool isSaved;

    public void SaveData(List<int> saveList)
    {
        if (!isSaved)
            isSaved = true;

        savedNumberObjectList = saveList;
    }
    
    public List<int> LoadData()
    {
        return savedNumberObjectList;
    }

    public bool IsSaved()
    {
        return isSaved;
    }
}
