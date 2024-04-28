// リザルトの値を制御
using UnityEngine;

public class Result : MonoBehaviour
{
    // ゲームの勝者
    private JudgeManager.Winner winner;
    // ゲーム終了後のBoardState
    private CubeManager.CubeColor[,,] resultBoardState = new CubeManager.CubeColor[3, 3, 3];

    //------------各プロパティ

    public CubeManager.CubeColor[,,] ResultBoardState { get { return resultBoardState; } }

    public JudgeManager.Winner Winner { get { return winner; } }

    //------------

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

    // リザルトの初期化
    public void InitializeResult()
    {
        winner = JudgeManager.Winner.None;

        for (int i = 0; i < resultBoardState.GetLength(0); i++)
        {
            for (int j = 0; j < resultBoardState.GetLength(1); j++)
            {
                for (int k = 0; k < resultBoardState.GetLength(2); k++)
                {
                    resultBoardState[i, j, k] = CubeManager.CubeColor.None;
                }
            }
        }
    }

    // ゲームのリザルトをセット
    public void SetResult(CubeFaller[,,] boardState, JudgeManager.Winner judge)
    {
        winner = judge;

        for (int i = 0; i < resultBoardState.GetLength(0); i++)
        {
            for (int j = 0; j < resultBoardState.GetLength(1); j++)
            {
                for (int k = 0; k < resultBoardState.GetLength(2); k++)
                {
                    if (boardState[i, j, k])
                    {
                        resultBoardState[i, j, k] = boardState[i, j, k].CubeColor;
                    }
                }
            }
        }
    }
}
