// キューブ全体を制御
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    //------------参照するスクリプト

    private CubeFaller[,,] boardState = new CubeFaller[3, 3, 3];
    [SerializeField] private CubeRotater cubeRotater;

    //------------

    // キューブ生成のためのプレハブ
    [SerializeField] private GameObject[] Cubes;
    // キューブの色のEnum
    public enum CubeColor
    {
        Gray,
        Red,
        Blue
    }
    // 次に生成されるキューブが何色か
    private CubeColor nextCubeColor = CubeColor.Gray;

    //------------各プロパティ

    public CubeFaller[,,] BoardState
    {
        get { return boardState; }
        set { boardState = value; }
    }

    public bool HasRotated
    {
        get { return cubeRotater.HasRotated; }
        set { cubeRotater.HasRotated = value; }
    }

    public CubeColor GetNextCubeColor { get { return nextCubeColor; } }

    // 回転ボタンが押されたかどうか
    public bool IsRotated => cubeRotater.IsRotated;

    // ひとつ前に回転した方向
    public string PreRotate => cubeRotater.PreRotate;

    //------------

    // ランダムな座標を作成
    private (float, float) GenerateRandomPosition()
    {
        // -1～1までのランダムなインデックスを代入
        int xRandomIndex = Random.Range(-1, 2);
        int zRandomIndex = Random.Range(-1, 2);

        return (xRandomIndex, zRandomIndex);
    }

    // ランダムな場所に灰色のキューブを生成する
    public void GenerateGrayCube()
    {
        GenerateCube(GenerateRandomPosition(), nextCubeColor);
    }

    // 指定された座標に任意のキューブを生成
    public void GenerateCube((float, float) position, CubeColor _cubeColor)
    {
        GameObject obj = Cubes[(int)_cubeColor];
        GameObject newCube = Instantiate(obj);
        newCube.transform.parent = transform;
        newCube.transform.position = new Vector3(position.Item1, 1, position.Item2);
        
        SetCubeOnBoard(position, newCube);

        SwitchCubeColor();
    }

    // 生成されたキューブ（落下済）を配列にあらかじめ追加
    private void SetCubeOnBoard((float, float) position, GameObject newCube)
    {
        for (int i = 0; i < 3; i++)
        {
            if (boardState[(int)position.Item1 + 1, i, (int)position.Item2 + 1] == null)
            {
                boardState[(int)position.Item1 + 1, i, (int)position.Item2 + 1] = newCube.GetComponent<CubeFaller>();
                return;
            }
        }
    }

    // 次に生成されるキューブの色を切り替える
    private void SwitchCubeColor()
    {
        if (nextCubeColor == CubeColor.Red)
        {
            this.nextCubeColor = CubeColor.Blue;
        }
        else {
            this.nextCubeColor = CubeColor.Red;
        }
    }

    // すべてのキューブの落下処理を管理
    public void FallAllCube()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (boardState[i, j, k] != null)
                    {
                        if (!boardState[i, j, k].CheckHasFalled(BoardState, i, j, k))
                        {
                            boardState[i, j, k].FallCube();
                        }
                        else
                        {
                            boardState[i, j, k].transform.position = new Vector3(i - 1, j - 1, k - 1);
                            boardState[i, j, k].HasFallen = true;
                        }
                    }
                }
            }
        }
    }

    // キューブの回転処理を管理
    public void RotateAllCube()
    {
        ResetAllCube();
        BoardState = cubeRotater.RotateCube(cubeRotater.Direction, BoardState);
        cubeRotater.IsRotated = false;
    }

    // すべてのキューブが落下済みかどうかを判定
    public bool AllHasFalled()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (boardState[i, j, k] != null)
                    {
                        if (!boardState[i, j, k].HasFallen)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    // すべてのキューブを未落下に設定
    public void ResetAllCube()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (boardState[i, j, k] != null)
                    {
                        boardState[i, j, k].HasFallen = false;
                    }
                }
            }
        }
    }
}
