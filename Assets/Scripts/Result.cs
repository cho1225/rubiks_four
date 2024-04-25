// リザルトの値を制御
using UnityEngine;

public class Result : MonoBehaviour
{
    // ゲームの勝者
    private int winner;
    // ゲーム終了後のBoardState
    private int[,,] resultBoardState = new int[3, 3, 3];

    // DontDestroyOnLoadの設定
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Result");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    //------------各プロパティ

    public int[,,] ResultBoardState { get { return resultBoardState; } }

    public int Winner { get { return winner; } }

    //------------

    // リザルトの初期化
    public void InitializeResult()
    {
        winner = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    resultBoardState[i, j, k] = 9;
                }
            }
        }
    }

    // ゲームのリザルトをセット
    public void SetResult(CubeFaller[,,] boardState, JudgeManager.Winner judge)
    {
        if (judge == JudgeManager.Winner.Red)
        {
            winner = 1;
        }
        else if (judge == JudgeManager.Winner.Blue)
        {
            winner = 2;
        }
        else
        {
            winner = 3;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (boardState[i, j, k])
                    {
                        if (boardState[i, j, k].GetCubeColor == CubeManager.CubeColor.Red)
                        {
                            resultBoardState[i, j, k] = 1;
                        }
                        else if (boardState[i, j, k].GetCubeColor == CubeManager.CubeColor.Blue)
                        {
                            resultBoardState[i, j, k] = 2;
                        }
                        else if (boardState[i, j, k].GetCubeColor == CubeManager.CubeColor.Gray)
                        {
                            resultBoardState[i, j, k] = 0;
                        }
                    }
                }
            }
        }
    }
}
