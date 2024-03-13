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
        for(int i = 0; i < rowCount; i++)
        {
            for (int k = 0; k < columnCount - 1; k++) // 1den fazla boþluk olduðunda boþ kalan yer oluyordu
            {
                for (int j = 0; j < columnCount - 1; j++)
                {
                    BoardObject currentBoardObject = boardDictionary[new Vector2Int(i, j)];

                    if (currentBoardObject.NumberObject != null) continue;

                    BoardObject nextBoardObject = boardDictionary[new Vector2Int(i, j + 1)];

                    if (nextBoardObject.NumberObject == null) continue;

                    nextBoardObject.NumberObject.PlayNumberObjectAnimation(currentBoardObject.transform, 0.1f, false);
                    nextBoardObject.NumberObject.transform.parent = currentBoardObject.transform;
                    currentBoardObject.NumberObject = nextBoardObject.NumberObject;
                    nextBoardObject.NumberObject = null;
                }
            }
        }

        onDropFinished?.Invoke(boardDictionary);
    }
}
