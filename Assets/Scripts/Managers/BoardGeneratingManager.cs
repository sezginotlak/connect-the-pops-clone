using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BoardGeneratingManager : MonoBehaviour
{
    #region InjectRegion
    [Inject]
    BoardManager boardManager;

    [Inject]
    NumberDataHolder numberDataHolder;

    [Inject]
    DropManager dropManager;

    [Inject]
    SaveManager saveManager;
    #endregion

    private void Awake()
    {
        boardManager.onDictionaryFilled += FillBoard;
        boardManager.onApplicationQuitOrPaused += SaveGame;
        dropManager.onDropFinished += CreateNumbersForEmptyBoardObjects;
    }

    void SaveGame(Dictionary<Vector2Int, BoardObject> boardObjectDict)
    {
        saveManager.SaveBoard(boardObjectDict);
    }

    void FillBoard(Dictionary<Vector2Int, BoardObject> boardObjectDict)
    {
        if (saveManager.IsBoardSaved())
        {
            saveManager.LoadBoard(boardObjectDict);
        }
        else
        {
            CreateNumbersForAllBoardObjects(boardObjectDict);
        }
    }

    void CreateNumbersForEmptyBoardObjects(Dictionary<Vector2Int, BoardObject> boardObjectDict)
    {
        foreach (var boardObjectPair in boardObjectDict)
        {
            if (boardObjectPair.Value.NumberObject != null) continue;

            BoardObject boardObject = boardObjectPair.Value;
            List<NumberData> numberDataList = numberDataHolder.numberDatas.numberDataList.Where(x => x.canBeCreated).ToList();
            AbstractBaseNumberObject createdNumberObject = Instantiate(numberDataList[Random.Range(0, numberDataList.Count)].prefab);

            createdNumberObject.transform.parent = boardObject.transform;
            Vector3 localScale = createdNumberObject.transform.localScale;
            createdNumberObject.transform.localPosition = Vector3.zero;
            createdNumberObject.transform.localScale = Vector3.zero;
            createdNumberObject.transform.DOScale(localScale, 0.3f);
            boardObject.NumberObject = createdNumberObject;
        }
    }

    void CreateNumbersForAllBoardObjects(Dictionary<Vector2Int, BoardObject> boardObjectDict)
    {
        foreach (var boardObjectPair in boardObjectDict)
        {
            BoardObject boardObject = boardObjectPair.Value;
            List<NumberData> numberDataList = numberDataHolder.numberDatas.numberDataList.Where(x => x.canBeCreated).ToList();
            AbstractBaseNumberObject createdNumberObject = Instantiate(numberDataList[Random.Range(0, numberDataList.Count)].prefab);

            createdNumberObject.transform.parent = boardObject.transform;
            createdNumberObject.transform.localPosition = Vector3.zero;
            boardObject.NumberObject = createdNumberObject;
        }
    }

    public void CreateNumberObject(BoardObject boardObject, int value)
    {
        AbstractBaseNumberObject prefab = numberDataHolder.numberDatas.numberDataList.Where(x => value >= x.minValue && value < x.maxValue).FirstOrDefault().prefab;
        AbstractBaseNumberObject createdNumberObject = Instantiate(prefab);

        createdNumberObject.transform.parent = boardObject.transform;
        createdNumberObject.transform.localPosition = Vector3.zero;
        boardObject.NumberObject = createdNumberObject;
    }
}
