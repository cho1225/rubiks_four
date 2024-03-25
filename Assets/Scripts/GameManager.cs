using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PanelManager panelManager;
    [SerializeField] private CubeManager cubeManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private JudgeManager judgeManager;
    [SerializeField] private ChangeScene changeScene;
    private Result result;

    private string[] gameState = { "CanPush", "Falling", "Judge", "Rotate", "Falling", "Judge" };
    private int gameStateNumber = 4;

    void Start()
    {
        result = GameObject.Find("Result").GetComponent<Result>();
        result.InitializeResult();
    }

    void Update()
    {
        if (gameState[gameStateNumber] == "CanPush")
        {
            judgeManager.HasJudge = false;
            if (panelManager.IsPushes())
            {
                SetGameState();
                panelManager.SetPushes();
                cubeManager.GenerateCube(panelManager.XZ, cubeManager.NextCubeColorIndex);
            }
            panelManager.EnabledAllPanel(gameState[gameStateNumber], cubeManager.BoardState);
        }

        if (gameState[gameStateNumber] == "Falling")
        {
            panelManager.EnabledAllPanel(gameState[gameStateNumber], cubeManager.BoardState);
            cubeManager.CubeRotater.HasRotated = false;
            cubeManager.FallAllCube();
            if (cubeManager.AllHasFalled())
            {
                SetGameState();
            }
        }

        if (gameState[gameStateNumber] == "Rotate")
        {
            judgeManager.HasJudge = false;
            uiManager.SetInteractiveButton(cubeManager.CubeRotater.PreRotate);
            uiManager.SetBottunActive(true);
            if (cubeManager.CubeRotater.IsRotated)
            {
                uiManager.SetWakuActive(false);
                cubeManager.ResetAllCube();
                cubeManager.BoardState = cubeManager.CubeRotater.Rotate(cubeManager.CubeRotater.Direction, cubeManager.BoardState);
                cubeManager.CubeRotater.IsRotated = false;
            }
            if (cubeManager.CubeRotater.HasRotated)
            {
                uiManager.SetWakuActive(true);
                uiManager.SetBottunActive(false);
                SetGameState();
            }
        }

        if (gameState[gameStateNumber] == "Judge")
        {
            if (judgeManager.CheckWinner(cubeManager.BoardState) != "done") 
            {   
                result.SetResult(cubeManager.BoardState, judgeManager.CheckWinner(cubeManager.BoardState));
                changeScene.Load("ResultScene");
            }
            else
            {
                SetGameState();
            }
        }
    }

    private void SetGameState()
    {
        gameStateNumber = (gameStateNumber + 1) % 6;
    }
}
