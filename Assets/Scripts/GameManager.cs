// インゲーム全体の進行を制御
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //------------参照するスクリプト

    [SerializeField] private PanelManager panelManager;
    [SerializeField] private CubeManager cubeManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private JudgeManager judgeManager;
    [SerializeField] private ChangeScene changeScene;
    private Result result;

    //------------

    // ゲームフェーズのEnum
    public enum GameState
    {
        CanPush, 
        Falling1, 
        Judge1,
        Rotate, 
        Falling2,
        Judge2
    }
    // 現在のゲームフェーズ
    private GameState gameState = GameState.Falling2;

    // 各スクリプトのStart関数は順番が保証されていないので一括で初期化
    void Start()
    {
        // DontDestroyOnLoadを使用しているため、Findで探す必要がある
        GameObject resultObject = GameObject.Find("Result");
        if (resultObject != null)
        {
            result = resultObject.GetComponent<Result>();
            if (result == null)
            {
                Debug.Log("Resultコンポーネントが見つかりません");
                return;
            }
        }
        else
        {
            Debug.Log("Resultオブジェクトが見つかりません");
            return;
        }
        result.InitializeResult();
        // Panelを初期化
        panelManager.InitializePanels();
        // 最初に灰色のキューブを追加
        cubeManager.GenerateGrayCube();
    }

    // メイン処理
    void Update()
    {
        switch(gameState)
        {
            case GameState.CanPush:
                panelManager.EnabledAllPanel(gameState, cubeManager.BoardState);

                // パネルが押されたときの処理
                if (panelManager.IsPushes())
                {
                    panelManager.SetPushes();
                    cubeManager.GenerateCube(panelManager.XZ, cubeManager.NextCubeColor);
                    SetGameState(GameState.Falling1);
                }
                break;
            case GameState.Falling1:
            case GameState.Falling2:
                panelManager.EnabledAllPanel(gameState, cubeManager.BoardState);
                cubeManager.HasRotated = false;
                cubeManager.FallAllCube();

                // キューブが全て落ちた後の処理
                if (cubeManager.AllHasFalled())
                {
                    // 回転前の落下か
                    if (gameState == GameState.Falling1)
                    {
                        SetGameState(GameState.Judge1);
                    }
                    // プッシュ前の落下か
                    if (gameState == GameState.Falling2)
                    {
                        SetGameState(GameState.Judge2);
                    }
                }
                break;
            case GameState.Rotate:
                // 回転のボタンが押されたときの処理
                if (cubeManager.IsRotated)
                {
                    uiManager.SetWakuActive(false);
                    uiManager.SetBottunActive(false);
                    cubeManager.RotateAllCube();
                }

                // 回転が終わった後の処理
                if (cubeManager.HasRotated)
                {
                    uiManager.SetWakuActive(true);
                    uiManager.SetBottunActive(false);
                    SetGameState(GameState.Falling2);
                }
                break;
            case GameState.Judge1:
            case GameState.Judge2:
                // 勝者がいるかどうか
                if (judgeManager.CheckWinner(cubeManager.BoardState) != JudgeManager.Winner.None)
                {
                    result.SetResult(cubeManager.BoardState, judgeManager.CheckWinner(cubeManager.BoardState));
                    changeScene.Load("ResultScene");
                }
                else
                {
                    // 回転前のジャッジか
                    if (gameState == GameState.Judge1)
                    {
                        judgeManager.HasJudge = false;
                        uiManager.SetInteractiveButton(cubeManager.PreRotate);
                        uiManager.SetBottunActive(true);
                        SetGameState(GameState.Rotate);
                    }
                    // プッシュ前のジャッジか
                    if (gameState == GameState.Judge2)
                    {
                        judgeManager.HasJudge = false;
                        uiManager.SetText(cubeManager.NextCubeColor);
                        SetGameState(GameState.CanPush);
                    }
                }
                break;
        }
    }

    // フェーズを次に進める
    private void SetGameState(GameState _gameState)
    {
        gameState = _gameState;
    }
}
