using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseNumberObject : MonoBehaviour
{
    [SerializeField] int value;
    public int Value { get => value; }

    // moves the number object to merge point
    public void PlayNumberObjectAnimation(Transform targetPoint, float duration)
    {
        transform.DOMove(targetPoint.position, duration);
    }
}
