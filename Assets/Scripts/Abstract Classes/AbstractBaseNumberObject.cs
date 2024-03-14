using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseNumberObject : MonoBehaviour
{
    [SerializeField] 
    int value;

    [SerializeField]
    float squishYPos = -0.03f;

    [SerializeField]
    float squishYScale = 0.94f;

    public int Value { get => value; }
    public Color Color { get; set; }

    private void Start()
    {
        Color = GetComponent<SpriteRenderer>().color;    
    }

    // moves the number object to target point
    public void PlayNumberObjectAnimation(Transform targetPoint, float duration, bool shouldDestroy = true)
    {
        transform.DOMove(targetPoint.position, duration).OnComplete(() => 
        {
            // shouldDestroy decides if it is dropped or merged, if merged then destroys otherwise plays drop animation
            if (shouldDestroy)
                Destroy(gameObject);
            else
                PlaySquishAnimation(0.25f);
        });
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
