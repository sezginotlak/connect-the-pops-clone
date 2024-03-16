using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DropManager : MonoBehaviour
{
    [Inject]
    LinkManager linkManager;

    public Action<Dictionary<Vector2Int, BoardObject>> onDropFinished;

    private void Awake()
    {
        linkManager.onMergeFinished += DropNumberObjects;
    }

    void DropNumberObjects(Dictionary<Vector2Int, BoardObject> boardDictionary)
    {
        int rowCount = (int)Mathf.Sqrt(boardDictionary.Count); //used this because we already know board is square
        int columnCount = rowCount;
        for(int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            for (int columnIterateCount = 0; columnIterateCount < columnCount - 1; columnIterateCount++) // iterates column about row count to make sure there is no null board object
            {
                for (int columnIndex = 0; columnIndex < columnCount - 1; columnIndex++)
                {
                    BoardObject currentBoardObject = boardDictionary[new Vector2Int(rowIndex, columnIndex)];

                    if (currentBoardObject.NumberObject != null) continue;

                    BoardObject nextBoardObject = boardDictionary[new Vector2Int(rowIndex, columnIndex + 1)];

                    if (nextBoardObject.NumberObject == null) continue;

                    nextBoardObject.NumberObject.PlayDropAnimation(currentBoardObject.transform, 0.15f);
                    nextBoardObject.NumberObject.transform.parent = currentBoardObject.transform;
                    currentBoardObject.NumberObject = nextBoardObject.NumberObject;
                    nextBoardObject.NumberObject = null;
                }
            }
        }

        onDropFinished?.Invoke(boardDictionary);
    }
}
