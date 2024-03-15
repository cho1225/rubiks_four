using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool hasFalled = false;
    public string cubeColor;

    public void SetHasFalled(bool _bool)
    {
        hasFalled = _bool;
    }

    public bool GetHasFalled()
    {
        return hasFalled;
    }

    public string GetCubeColor() { return cubeColor; }
}
