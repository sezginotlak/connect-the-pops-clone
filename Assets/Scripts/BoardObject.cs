using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObject : MonoBehaviour
{
    public AbstractBaseNumberObject NumberObject { get; set; }
    public Vector2Int boardPosition;
}
