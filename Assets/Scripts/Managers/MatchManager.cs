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

        for(int i = 0; i < numberList.Count; i++)
        {
            AbstractBaseNumberObject numberObject = numberList[i];
            BoardObject boardObject = boardDictionary.ElementAt(i).Value;
            numberObject.transform.parent = boardObject.transform;
            numberObject.transform.localPosition = Vector3.zero;
            boardObject.NumberObject = numberObject;
        }
    }
}
