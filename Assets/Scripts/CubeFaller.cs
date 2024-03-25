using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeFaller : MonoBehaviour
{
    private bool hasFalled = false;
    private float speed = 1.0f;
    [SerializeField] private int cubeColorIndex;

    public bool HasFalled
    {
        get { return hasFalled; }
        set { hasFalled = value; }
    }

    public int CubeColorIndex { get { return cubeColorIndex; } }

    public void FallCube()
    {
        this.transform.Translate(0, -speed * Time.deltaTime, 0, Space.World);
    }

    public bool CheckHasFalled(CubeFaller[,,] boardState, int i, int j, int k)
    {
        if (j == 0)
        {
            if (boardState[i, j, k].transform.position.y > -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (boardState[i, j, k].transform.position.y - 1 > boardState[i, j - 1, k].transform.position.y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
