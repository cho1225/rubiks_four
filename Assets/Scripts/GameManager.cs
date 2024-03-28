using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //------------�Q�Ƃ���X�N���v�g

    [SerializeField] private PanelManager panelManager;
    [SerializeField] private CubeManager cubeManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private JudgeManager judgeManager;
    [SerializeField] private ChangeScene changeScene;
    private Result result;

    //------------

    // �Q�[���t�F�[�Y�̔z��
    private string[] gameState = { "CanPush", "Falling", "Judge", "Rotate", "Falling", "Judge" };
    // ���݂̃Q�[���t�F�[�Y
    private int gameStateNumber = 4;

    // �ŏ���Result�R���|�[�l���g���Ƃ��Ă��ď���������
    void Start()
    {
        // DontDestroyOnLoad���g�p���Ă��邽�߁AFind�ŒT���K�v������
        result = GameObject.Find("Result").GetComponent<Result>();
        result.InitializeResult();
    }

    // ���C������
    void Update()
    {
        // �p�l���������t�F�[�Y
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
        // �L���[�u�̗����t�F�[�Y
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
        // �L���[�u�̉�]�t�F�[�Y
        if (gameState[gameStateNumber] == "Rotate")
        {
            judgeManager.HasJudge = false;
            uiManager.SetInteractiveButton(cubeManager.CubeRotater.PreRotate);
            uiManager.SetBottunActive(true);
            if (cubeManager.CubeRotater.IsRotated)
            {
                uiManager.SetWakuActive(false);
                cubeManager.ResetAllCube();
                cubeManager.BoardState = cubeManager.CubeRotater.RotateCube(cubeManager.CubeRotater.Direction, cubeManager.BoardState);
                cubeManager.CubeRotater.IsRotated = false;
            }
            if (cubeManager.CubeRotater.HasRotated)
            {
                uiManager.SetWakuActive(true);
                uiManager.SetBottunActive(false);
                SetGameState();
            }
        }
        // ��������t�F�[�Y
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

    // �t�F�[�Y�����ɐi�߂�
    private void SetGameState()
    {
        gameStateNumber = (gameStateNumber + 1) % 6;
    }
}