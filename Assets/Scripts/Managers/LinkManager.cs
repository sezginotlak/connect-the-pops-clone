using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LinkManager : MonoBehaviour
{
    #region InjectRegion
    [Inject]
    InputManager inputManager;

    [Inject]
    BoardManager boardManager;

    [Inject]
    NumberDataHolder numberDataHolder;

    [Inject]
    LineRendererManager lineRendererManager;
    #endregion

    List<AbstractBaseNumberObject> numberObjectList = new List<AbstractBaseNumberObject>();
    List<BoardObject> boardObjectList = new List<BoardObject>();

    private void Update()
    {
        if (inputManager.IsPressing())
            Link();

        if (inputManager.IsPressFinished())
            Merge();
    }

    void AddNumberToList(BoardObject boardObject)
    {
        if (numberObjectList.Count > 0 && numberObjectList[^1].Value != boardObject.NumberObject.Value) return;

        if (boardObjectList.Count > 0 && !boardManager.IsNeighbour(boardObject.boardPosition, boardObjectList[^1].boardPosition)) return;

        boardObjectList.Add(boardObject);
        numberObjectList.Add(boardObject.NumberObject);
        lineRendererManager.AddPoint(boardObject.transform.position, boardObject.NumberObject.Color);
    }

    void RemoveNumberFromList(BoardObject boardObject)
    {
        if (numberObjectList.IndexOf(boardObject.NumberObject) != numberObjectList.Count - 2) return;

        boardObjectList.Remove(boardObjectList[^1]);
        numberObjectList.Remove(numberObjectList[^1]);
        lineRendererManager.RemovePoint();
    }

    // decides if number is gonna be added or removed
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

    void Merge()
    {
        if (numberObjectList.Count < 2) return;

        StartCoroutine(IEMerge());
    }
    
    IEnumerator IEMerge()
    {
        lineRendererManager.ClearAllPoints();

        int total = numberObjectList.Count * numberObjectList[^1].Value;
        List<NumberData> list = numberDataHolder.numberDatas.numberDataList;
        NumberData numberData = list.Where(data => total >= data.minValue && total < data.maxValue).FirstOrDefault();
        AbstractBaseNumberObject prefab = numberData.prefab;

        BoardObject parentBoardObject = boardObjectList[^1];

        foreach(AbstractBaseNumberObject number in numberObjectList)
        {
            number.PlayNumberObjectAnimation(parentBoardObject.transform, 0.1f);
        }

        foreach(BoardObject boardObject in boardObjectList)
        {
            boardObject.NumberObject = null;
        }

        yield return new WaitForSeconds(0.1f);

        numberObjectList.Clear();
        boardObjectList.Clear();

        AbstractBaseNumberObject instantiated = Instantiate(prefab);
        instantiated.transform.parent = parentBoardObject.transform;
        instantiated.transform.localPosition = Vector3.zero;
        instantiated.transform.DOPunchScale(instantiated.transform.localScale * 0.05f, 0.1f);
        parentBoardObject.NumberObject = instantiated;
    }
}
