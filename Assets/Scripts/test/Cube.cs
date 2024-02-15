using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool hasFalled = false;

    public void SetHasFalled(bool _bool)
    {
        hasFalled = _bool;
    }

    public bool GetHasFalled()
    {
        return hasFalled;
    }
}
