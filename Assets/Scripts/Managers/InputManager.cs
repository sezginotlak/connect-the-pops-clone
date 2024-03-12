using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool IsPressStarted()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool IsPressing()
    {
        return Input.GetMouseButton(0);
    }

    public bool IsPressFinished()
    {
        return Input.GetMouseButtonUp(0);
    }

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
}
