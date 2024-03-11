using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LinkManager : MonoBehaviour
{
    [Inject]
    InputManager inputManager;

    List<AbstractBaseNumberObject> numberObjectList = new List<AbstractBaseNumberObject>();

    void AddNumberToQueue(AbstractBaseNumberObject numberObject)
    {
        if (numberObjectList.Contains(numberObject)) return;

        if (numberObjectList[^1].Value != numberObject.Value) return;

        numberObjectList.Add(numberObject);
    }
}
