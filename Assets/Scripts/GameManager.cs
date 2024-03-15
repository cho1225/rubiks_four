using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PanelManager panelManager;
    [SerializeField] private CubeManager cubeManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private RotateManager rotateManager;
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
            judgeManager.SetHasJudge();
            if (panelManager.IsPushes())
            {
                SetGameState();
                panelManager.SetPushes(false);
                cubeManager.GenerateCube(panelManager.GetXZ(), cubeManager.GetCubeColorIndex());
            }
            panelManager.EnabledAllPanel(gameState[gameStateNumber], cubeManager.GetBoardState());
        }

        if (gameState[gameStateNumber] == "Falling")
        {
            panelManager.EnabledAllPanel(gameState[gameStateNumber], cubeManager.GetBoardState());
            rotateManager.SetAllHasRotate(false);
            cubeManager.MoveCube();
            if (cubeManager.AllHasFalled())
            {
                SetGameState();
            }
        }

        if (gameState[gameStateNumber] == "Rotate")
        {
            judgeManager.SetHasJudge();
            uiManager.SetInteractiveButton(rotateManager.GetPreRotate());
            uiManager.SetBottunActive(true);
            if (rotateManager.GetHasRotated())
            {
                uiManager.SetWakuActive(false);
                cubeManager.ResetAllCube();
                cubeManager.SetBoardState(rotateManager.Rotate(rotateManager.GetDirection(), cubeManager.GetBoardState()));
                rotateManager.SetHasRotated(false);
            }
            if (rotateManager.GetAllHasRotate())
            {
                uiManager.SetWakuActive(true);
                uiManager.SetBottunActive(false);
                SetGameState();
            }
        }

        if (gameState[gameStateNumber] == "Judge")
        {
            if (judgeManager.CheckWinner(cubeManager.GetBoardState()) != "done") 
            {   
                result.SetResult(cubeManager.GetBoardState(), judgeManager.CheckWinner(cubeManager.GetBoardState()));
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
