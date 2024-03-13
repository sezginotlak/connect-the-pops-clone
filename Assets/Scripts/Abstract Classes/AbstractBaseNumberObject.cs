using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseNumberObject : MonoBehaviour
{
    [SerializeField] int value;
    public int Value { get => value; }
    public Color Color { get; set; }

    private void Start()
    {
        Color = GetComponent<SpriteRenderer>().color;    
    }

    // moves the number object to merge point
    public void PlayNumberObjectAnimation(Transform targetPoint, float duration, bool shouldDestroy = true)
    {
        transform.DOMove(targetPoint.position, duration).OnComplete(() => 
        {
            if (shouldDestroy) 
                Destroy(gameObject);
        });
    }
}
