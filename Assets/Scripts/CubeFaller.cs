// キューブの落下を制御
using UnityEngine;

public class CubeFaller : MonoBehaviour
{
    // このキューブが落下済みか
    private bool hasFallen = false;
    // 落下のスピード
    private float speed = 1.0f;
    // このキューブが何色か
    [SerializeField] private CubeManager.CubeColor cubeColor;

    //------------各プロパティ

    public bool HasFallen
    {
        get { return hasFallen; }
        set { hasFallen = value; }
    }

    public CubeManager.CubeColor GetCubeColor { get { return cubeColor; } }

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
