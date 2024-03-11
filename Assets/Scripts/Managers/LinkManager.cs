using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LinkManager : MonoBehaviour
{
    [Inject]
    InputManager inputManager;

    [Inject]
    BoardManager boardManager;

    List<AbstractBaseNumberObject> numberObjectList = new List<AbstractBaseNumberObject>();
    List<BoardObject> boardObjectList = new List<BoardObject>();

    void AddNumberToList(BoardObject boardObject)
    {
        if (boardManager.IsNeighbour(boardObject.boardPosition, boardObjectList[^1].boardPosition)) return;

        if (numberObjectList.Contains(boardObject.NumberObject)) return;

        if (numberObjectList.Count > 0 && numberObjectList[^1].Value != boardObject.NumberObject.Value) return;

        boardObjectList.Add(boardObject);
        numberObjectList.Add(boardObject.NumberObject);
    }
}
