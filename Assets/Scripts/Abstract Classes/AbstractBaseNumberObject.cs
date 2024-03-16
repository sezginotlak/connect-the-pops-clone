using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseNumberObject : MonoBehaviour
{
    [SerializeField] 
    int value;

    [SerializeField]
    Color color;

    [SerializeField]
    float squishYPos = -0.03f;

    [SerializeField]
    float squishYScale = 0.94f;

    public int Value { get => value; }
    public Color Color { get => color; }

    public void PlayMergeAnimation(Transform targetPoint, float duration)
    {
        transform.DOMove(targetPoint.position, duration).OnComplete(() => 
        {
            Destroy(gameObject);
        });
    }

    public void PlayDropAnimation(Transform targetPoint, float duration)
    {
        transform.DOMove(targetPoint.position, duration).OnComplete(() =>
        {
            PlaySquishAnimation(0.25f);
        });
    }

    public void PlayMovementAnimation(Transform targetPoint, float duration)
    {
        transform.DOMove(targetPoint.position, duration);
    }

    public void PlaySquishAnimation(float duration)
    {
        transform.DOLocalMoveY(squishYPos, duration / 2f).OnComplete(() =>
        {
            transform.DOLocalMoveY(0, duration / 2f);
        });

        transform.DOScaleY(squishYScale, duration / 2f).OnComplete(() =>
        {
            transform.DOScaleY(1f, duration / 2f);
        });
    }
}
