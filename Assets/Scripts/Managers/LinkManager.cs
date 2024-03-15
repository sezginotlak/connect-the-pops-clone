using DG.Tweening;
using System;
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

    [SerializeField]
    Transform mergePreviewParent;
    AbstractBaseNumberObject instantiatedPreview;

    public Action<Dictionary<Vector2Int, BoardObject>> onMergeFinished;

    private void Update()
    {
        if (inputManager.IsPressing())
            Link();

        if (inputManager.IsPressFinished())
        {
            Merge();
            DestroyMergePreview();
            lineRendererManager.ClearAllPoints();
        }
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

    void ClearLists()
    {
        boardObjectList.Clear();
        numberObjectList.Clear();
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

        AdjustMergePreview();
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
        if (numberObjectList.Count < 2)
        {
            ClearLists();
            return;
        }

        StartCoroutine(IEMerge());
    }
    
    IEnumerator IEMerge()
    {
        AbstractBaseNumberObject prefab = GetPrefabWillInstantiated();

        if (prefab == null) 
            yield break;

        BoardObject parentBoardObject = boardObjectList[^1];

        foreach(AbstractBaseNumberObject number in numberObjectList)
        {
            number.PlayMergeAnimation(parentBoardObject.transform, 0.2f);
        }

        foreach(BoardObject boardObject in boardObjectList)
        {
            boardObject.NumberObject = null;
        }

        yield return new WaitForSeconds(0.2f);

        ClearLists();

        AbstractBaseNumberObject instantiated = Instantiate(prefab);
        instantiated.transform.parent = parentBoardObject.transform;
        instantiated.transform.localPosition = Vector3.zero;
        instantiated.transform.DOPunchScale(instantiated.transform.localScale * 0.05f, 0.1f);
        parentBoardObject.NumberObject = instantiated;
        onMergeFinished?.Invoke(boardManager.GetBoardDataDictionary());
    }

    AbstractBaseNumberObject GetPrefabWillInstantiated()
    {
        int total = numberObjectList.Count * numberObjectList[^1].Value;
        List<NumberData> list = numberDataHolder.numberDatas.numberDataList;

        if (total >= list[^1].maxValue) 
            return null;

        NumberData numberData = list.Where(data => total >= data.minValue && total < data.maxValue).FirstOrDefault();
        return numberData.prefab;
    }

    void AdjustMergePreview()
    {
        AbstractBaseNumberObject mergePreview = GetPrefabWillInstantiated();

        if (mergePreview == null)
            return;

        if (instantiatedPreview != null)
            DestroyMergePreview();

        instantiatedPreview = Instantiate(mergePreview);

        instantiatedPreview.transform.parent = mergePreviewParent.transform;
        instantiatedPreview.transform.localScale = Vector3.one;
        instantiatedPreview.transform.localPosition = Vector3.zero;
    }

    void DestroyMergePreview()
    {
        if (instantiatedPreview != null)
        {
            Destroy(instantiatedPreview.gameObject);
            instantiatedPreview = null;
        }
    }
}
