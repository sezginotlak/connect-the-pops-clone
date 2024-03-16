using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MatchManager : MonoBehaviour
{
    [Inject]
    BoardManager boardManager;

    [SerializeField]
    Button orderButton;

    private void Awake()
    {
        orderButton.onClick.AddListener(OrderBoard);
    }

    void OrderBoard()
    {
        Dictionary<Vector2Int, BoardObject> boardDictionary = boardManager.GetBoardDataDictionary();
        List<AbstractBaseNumberObject> numberList = new List<AbstractBaseNumberObject>();

        foreach(KeyValuePair<Vector2Int, BoardObject> boardObject in boardDictionary)
        {
            numberList.Add(boardObject.Value.NumberObject);
        }

        numberList = numberList.OrderByDescending(number => number.Value).ToList();

        int rowCount = (int)Mathf.Sqrt(boardDictionary.Count);
        int columnCount = rowCount;

        // iterates the list left to right if rowIndex is even, otherwise right to left
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            int currentRow = rowIndex * rowCount; // to access numberList start index for different rows
            if (rowIndex % 2 == 0)
            {
                for(int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    int numberObjectIndex = currentRow + columnIndex;
                    AbstractBaseNumberObject numberObject = numberList[numberObjectIndex];
                    BoardObject boardObject = boardDictionary[new Vector2Int(columnIndex, rowIndex)];
                    numberObject.transform.parent = boardObject.transform;
                    numberObject.PlayMovementAnimation(boardObject.transform, 0.1f);
                    boardObject.NumberObject = numberObject;
                }
            }
            else
            {
                int maxColumnIndex = columnCount - 1;
                for (int columnIndex = maxColumnIndex; columnIndex >= 0; columnIndex--)
                {
                    int toBeAddedColumnIndex = maxColumnIndex - columnIndex;
                    int numberObjectIndex = currentRow + toBeAddedColumnIndex;
                    AbstractBaseNumberObject numberObject = numberList[numberObjectIndex];
                    BoardObject boardObject = boardDictionary[new Vector2Int(columnIndex, rowIndex)];
                    numberObject.transform.parent = boardObject.transform;
                    numberObject.PlayMovementAnimation(boardObject.transform, 0.1f);
                    boardObject.NumberObject = numberObject;
                }
            }
        }
    }
}
