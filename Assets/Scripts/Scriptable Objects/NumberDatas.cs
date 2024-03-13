using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NumberDatasSO", order = 1)]
public class NumberDatas : ScriptableObject
{
    public List<NumberData> numberDataList = new List<NumberData>();
}

[Serializable]
public class NumberData
{
    public int minValue;
    public int maxValue;
    public AbstractBaseNumberObject prefab;
    public bool isOpen;
}
