using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    bool isDictionaryFilled;
    Dictionary<Vector2Int, BoardObject> boardDataDictionary;

    [SerializeField] Transform boardParentObject;

    public Action<Dictionary<Vector2Int, BoardObject>> onDictionaryFilled;

    private void Start()
    {
        FillBoardDataDictionary(boardParentObject);
    }

    void FillBoardDataDictionary(Transform boardParentObject)
    {
        StartCoroutine(IEFillBoardDataDictionary(boardParentObject));
    }

    IEnumerator IEFillBoardDataDictionary(Transform boardParentObject)
    {
        isDictionaryFilled = false;
        boardDataDictionary = new Dictionary<Vector2Int, BoardObject>();

        int childCount = boardParentObject.childCount;

        for(int i = 0; i < childCount; i++)
        {
            boardParentObject.GetChild(i).TryGetComponent(out BoardObject boardObject);

            if (boardObject == null) continue;

            boardDataDictionary.Add(boardObject.boardPosition, boardObject);
        }

        isDictionaryFilled = true;

        yield return new WaitUntil(IsDictionaryFilled);

        onDictionaryFilled?.Invoke(boardDataDictionary);
    }

    bool IsDictionaryFilled()
    {
        return isDictionaryFilled;
    }

    public BoardObject GetBoardObject(Vector2Int boardPosition)
    {
        return boardDataDictionary[boardPosition];
    }


    // checks if two board objects are neighbour
    public bool IsNeighbour(Vector2Int toBeAddedPosition, Vector2Int lastObjectPosition)
    {

        return true;
    }
}
