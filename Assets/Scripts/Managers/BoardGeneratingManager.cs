using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoardGeneratingManager : MonoBehaviour
{
    [Inject]
    BoardManager boardManager;
    [SerializeField] List<AbstractBaseNumberObject> numberPrefabList = new List<AbstractBaseNumberObject>();

    private void Awake()
    {
        boardManager.onDictionaryFilled += CreateNumbersForAllBoardObjects;
    }

    void CreateNumbersForAllBoardObjects(Dictionary<Vector2Int, BoardObject> boardObjectDict)
    {
        foreach (var boardObjectPair in boardObjectDict)
        {
            BoardObject boardObject = boardObjectPair.Value;

            AbstractBaseNumberObject createdNumberObject = Instantiate(numberPrefabList[Random.Range(0, numberPrefabList.Count)]);

            createdNumberObject.transform.parent = boardObject.transform;
            createdNumberObject.transform.localPosition = Vector3.zero;
            boardObject.NumberObject = createdNumberObject;
        }
    }
}
