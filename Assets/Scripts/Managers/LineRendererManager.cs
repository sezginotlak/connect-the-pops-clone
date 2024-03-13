using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager : MonoBehaviour
{
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void AddPoint(Vector3 point, Color color)
    {
        SetColor(color);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);
    }

    public void RemovePoint()
    {
        lineRenderer.positionCount--;
    }

    public void ClearAllPoints()
    {
        lineRenderer.positionCount = 0;
    }

    void SetColor(Color color)
    {
        lineRenderer.material.color = color;
    }
}
