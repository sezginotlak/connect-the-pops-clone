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

    [SerializeField] LayerMask hitLayer;

    private void Update()
    {
        if (inputManager.IsPressing())
            Link();
    }

    void AddNumberToList(BoardObject boardObject)
    {
        if (numberObjectList.Count > 0 && numberObjectList[^1].Value != boardObject.NumberObject.Value) return;

        //Debug.Log("Döndü 29'dan " + boardObject, boardObject);
        Debug.Log("Count: " + boardObjectList.Count);
        if (boardObjectList.Count > 0 && !boardManager.IsNeighbour(boardObject.boardPosition, boardObjectList[^1].boardPosition));

        boardObjectList.Add(boardObject);
        numberObjectList.Add(boardObject.NumberObject);
    }

    void RemoveNumberFromList(BoardObject boardObject)
    {
        if (numberObjectList.IndexOf(boardObject.NumberObject) != numberObjectList.Count - 2) return;

        boardObjectList.Remove(boardObjectList[^1]);
        numberObjectList.Remove(numberObjectList[^1]);
    }

    void HandleNumberOperation(BoardObject boardObject)
    {
        if (boardObject == null) return;

        if (boardObjectList.Count > 0 && boardObjectList[^1] == boardObject) return;

        if (!boardObjectList.Contains(boardObject))
        {
            AddNumberToList(boardObject);
        }
        else
        {
            RemoveNumberFromList(boardObject);
        }
    }

    void Link()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(inputManager.GetMousePosition()), Vector2.zero);

        if (hit.collider == null) return;

        hit.transform.TryGetComponent(out BoardObject boardObject);

        HandleNumberOperation(boardObject);
    }

    bool IsPowerOfTwo(int number)
    {
        return number != 0 && ((number & (number - 1)) == 0);
    }
}
