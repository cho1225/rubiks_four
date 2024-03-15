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

    private string[] gameState = { "CanPush", "Falling", "Judge", "Rotate", "Falling", "Judge" };
    private int gameStateNumber = 4;

    public static int winner = 0;
    public static int[,,] resultBoardState = new int[3, 3, 3];

    void Start()
    {
        InitializeResult();
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
                cubeManager.GenerateCube(panelManager.GetXZ(), cubeManager.cubeColor);
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
                SetResult(cubeManager.GetBoardState());
                Debug.Log(winner);
                this.GetComponent<ChangeScene>().Load("ResultScene");
            }
            else
            {
                SetGameState();
            }
        }
    }

    private void InitializeResult()
    {
        winner = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    resultBoardState[i, j, k] = 0;
                }
            }
        }
    }

    private void SetResult(GameObject[,,] boardState)
    {
        if (judgeManager.CheckWinner(boardState) == "red")
        {
            winner = 1;
        }
        else if (judgeManager.CheckWinner(boardState) == "blue")
        {
            winner = 2;
        }

        for (int i = 0;i < 3;i++)
        {
            for (int j=0;j < 3;j++)
            {
                for (int k = 0;k < 3;k++)
                {
                    if (boardState[i,j,k])
                    {
                        if (boardState[i,j,k].CompareTag("Red"))
                        {
                            resultBoardState[i, j, k] = 1;
                        }
                        else if (boardState[i, j, k].CompareTag("Blue"))
                        {
                            resultBoardState[i, j, k] = 2;
                        }
                        else if (boardState[i, j, k].CompareTag("Gray"))
                        {
                            resultBoardState[i, j, k] = 3;
                        }
                    }
                }
            }
        }
    }

    private void SetGameState()
    {
        gameStateNumber = (gameStateNumber + 1) % 6;
    }
}
