using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleButton : MonoBehaviour
{
    private void Start()
    {
        transform.DOPunchScale(transform.localScale * 0.1f, 1f, 0, 0).SetLoops(-1);
    }
}
