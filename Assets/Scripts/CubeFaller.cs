using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeFaller : MonoBehaviour
{
    // このキューブが落下済みか
    private bool hasFalled = false;
    // 落下のスピード
    private float speed = 1.0f;
    /* このキューブが何色か
       灰色は0、赤色は1、青色は2 */
    [SerializeField] private int cubeColorIndex;

    //------------各プロパティ

    public bool HasFalled
    {
        get { return hasFalled; }
        set { hasFalled = value; }
    }

    public int CubeColorIndex { get { return cubeColorIndex; } }

    //------------

    // キューブの落下処理
    public void FallCube()
    {
        this.transform.Translate(0, -speed * Time.deltaTime, 0, Space.World);
    }

    // キューブが落下済みかどうかを判定
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
