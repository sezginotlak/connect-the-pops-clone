using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    bool IsPressStarted()
    {
        return Input.GetMouseButtonDown(0);
    }

    bool IsPressing()
    {
        return Input.GetMouseButton(0);
    }

    bool IsPressFinished()
    {
        return Input.GetMouseButtonUp(0);
    }
}
