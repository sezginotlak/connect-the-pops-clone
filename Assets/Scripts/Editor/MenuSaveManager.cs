using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MenuSaveManager : MonoBehaviour
{
    [MenuItem("Save/Delete Save")]
    private static void DeleteSave()
    {
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        string savePath = Application.persistentDataPath + saveManager.BoardSavePath;

        if (!File.Exists(savePath)) return;

        File.Delete(savePath);
    }
}
