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

        for (int i = 0; i < rowCount; i++)
        {
            if (i % 2 == 0)
            {
                for(int j = 0; j < columnCount; j++)
                {
                    AbstractBaseNumberObject numberObject = numberList[i * rowCount + j];
                    BoardObject boardObject = boardDictionary[new Vector2Int(j, i)];
                    numberObject.transform.parent = boardObject.transform;
                    numberObject.PlayMovementAnimation(boardObject.transform, 0.1f);
                    boardObject.NumberObject = numberObject;
                }
            }
            else
            {
                for (int j = columnCount - 1; j >= 0; j--)
                {
                    AbstractBaseNumberObject numberObject = numberList[i * columnCount + (columnCount - 1 - j)];
                    BoardObject boardObject = boardDictionary[new Vector2Int(j, i)];
                    numberObject.transform.parent = boardObject.transform;
                    numberObject.PlayMovementAnimation(boardObject.transform, 0.1f);
                    boardObject.NumberObject = numberObject;
                }
            }
        }
    }
}
