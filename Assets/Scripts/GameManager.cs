using System.Collections;
using System.Collections.Generic;
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

    // ゲームフェーズの配列
    private string[] gameState = { "CanPush", "Falling", "Judge", "Rotate", "Falling", "Judge" };
    // 現在のゲームフェーズ
    private int gameStateNumber = 4;

    // 最初にResultコンポーネントをとってきて初期化する
    void Start()
    {
        // DontDestroyOnLoadを使用しているため、Findで探す必要がある
        result = GameObject.Find("Result").GetComponent<Result>();
        result.InitializeResult();
    }

    // メイン処理
    void Update()
    {
        // パネルを押すフェーズ
        if (gameState[gameStateNumber] == "CanPush")
        {
            if (panelManager.IsPushes())
            {
                SetGameState();
                panelManager.SetPushes();
                cubeManager.GenerateCube(panelManager.XZ, cubeManager.NextCubeColorIndex);
            }
            panelManager.EnabledAllPanel(gameState[gameStateNumber], cubeManager.BoardState);
        }
        // キューブの落下フェーズ
        if (gameState[gameStateNumber] == "Falling")
        {
            panelManager.EnabledAllPanel(gameState[gameStateNumber], cubeManager.BoardState);
            cubeManager.HasRotated = false;
            cubeManager.FallAllCube();
            if (cubeManager.AllHasFalled())
            {
                SetGameState();
            }
        }
        // キューブの回転フェーズ
        if (gameState[gameStateNumber] == "Rotate")
        {
            if (cubeManager.IsRotated)
            {
                uiManager.SetWakuActive(false);
                uiManager.SetBottunActive(false);
                cubeManager.RotateAllCube();
            }
            if (cubeManager.HasRotated)
            {
                uiManager.SetWakuActive(true);
                uiManager.SetBottunActive(false);
                SetGameState();
            }
        }
        // 勝利判定フェーズ
        if (gameState[gameStateNumber] == "Judge")
        {
            if (judgeManager.CheckWinner(cubeManager.BoardState) != "done") 
            {   
                result.SetResult(cubeManager.BoardState, judgeManager.CheckWinner(cubeManager.BoardState));
                changeScene.Load("ResultScene");
            }
            else
            {
                // 回転前のジャッジか
                if (gameStateNumber == 2)
                {
                    judgeManager.HasJudge = false;
                    uiManager.SetInteractiveButton(cubeManager.PreRotate);
                    uiManager.SetBottunActive(true);
                }
                // プッシュ前のジャッジか
                if (gameStateNumber == 5)
                {
                    judgeManager.HasJudge = false;
                    uiManager.SetText(cubeManager.NextCubeColorIndex);
                }
                SetGameState();
            }
        }
    }

    // フェーズを次に進める
    private void SetGameState()
    {
        gameStateNumber = (gameStateNumber + 1) % 6;
    }
}
