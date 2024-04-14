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
            cubeManager.HasRotated = false;
            cubeManager.FallAllCube();
            if (cubeManager.AllHasFalled())
            {
                SetGameState();
            }
        }
        // �L���[�u�̉�]�t�F�[�Y
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
                // ��]�O�̃W���b�W��
                if (gameStateNumber == 2)
                {
                    judgeManager.HasJudge = false;
                    uiManager.SetInteractiveButton(cubeManager.PreRotate);
                    uiManager.SetBottunActive(true);
                }
                // �v�b�V���O�̃W���b�W��
                if (gameStateNumber == 5)
                {
                    judgeManager.HasJudge = false;
                    uiManager.SetText(cubeManager.NextCubeColorIndex);
                }
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
