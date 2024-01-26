using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    //�}�E�X�I�[�o�[���̐F
    private Color MouseOverColor = Color.gray;

    //�I���W�i���̐F
    private Color OriginalColor;

    //�Q�[���I�u�W�F�N�g��Renderer
    MeshRenderer Renderer;

    public GameObject parentObject;

    GameController gamecontroller;
    public int PanelNumberX;
    public int PanelNumberY;
    public int[,,] panelBoardState;
    private bool onBoard = false;

    void Start()
    {
        //Renderer�R���|�[�l���g���Ƃ��Ă���
        Renderer = GetComponent<MeshRenderer>();
        //�I���W�i���̐F���Ƃ��Ă���
        OriginalColor = Renderer.material.color;

        GameObject gameObject = GameObject.Find("GameObject");
        gamecontroller = gameObject.GetComponent<GameController>();
        panelBoardState = new int[gamecontroller.boardSize, gamecontroller.boardSize, gamecontroller.boardSize];
    }

    public void PanelUpdate()
    {
        if (gamecontroller.gameState != "CanPush")
        {
            //�I���W�i���̐F�ɖ߂�
            Renderer.material.color = OriginalColor;
        }
        panelBoardState = gamecontroller.boardState;
    }

    public void OnClick()
    {
        if (gamecontroller.gameState == "CanPush" && gamecontroller.hasPush == false)
        {
            if (panelBoardState[PanelNumberX, PanelNumberY, gamecontroller.boardSize - 1] == 0)
            {
                // Cube�v���n�u��GameObject�^�Ŏ擾
                if (gamecontroller.cubeColorState == "Red")
                {
                    GameObject obj = (GameObject)Resources.Load("RedCube");
                    // Cube�v���n�u�����ɁA�C���X�^���X�𐶐��A
                    GameObject newCube = Instantiate(obj, parentObject.transform);
                    newCube.transform.position = new Vector3(transform.position.x, (gamecontroller.boardSize - 1) / 2, transform.position.z);
                    gamecontroller.spawnedCubes.Add(newCube);

                    CheckOnBoard(1);

                    gamecontroller.cubeColorState = "Blue";
                }
                else if (gamecontroller.cubeColorState == "Blue")
                {
                    GameObject obj = (GameObject)Resources.Load("BlueCube");
                    // Cube�v���n�u�����ɁA�C���X�^���X�𐶐��A
                    GameObject newCube = Instantiate(obj, parentObject.transform);
                    newCube.transform.position = new Vector3(transform.position.x, (gamecontroller.boardSize - 1) / 2, transform.position.z);
                    gamecontroller.spawnedCubes.Add(newCube);

                    CheckOnBoard(2);

                    gamecontroller.cubeColorState = "Red";
                }
                else
                {
                    Debug.Log("Error");
                    return;
                }
                gamecontroller.hasJudge = false;
                gamecontroller.hasPush = true;
            }
        }
    }

    void CheckOnBoard(int playerID)
    {
        onBoard = true;
        for (int i = 0; i < gamecontroller.boardSize; i++)
        {
            if (panelBoardState[PanelNumberX, PanelNumberY, i] == 0 && onBoard)
            {
                panelBoardState[PanelNumberX, PanelNumberY, i] = playerID;
                onBoard = false;
            }
        }
    }

    void OnMouseOver()
    {
        if (gamecontroller.gameState == "CanPush" && gamecontroller.hasPush == false)
        {
            //�}�E�X�I�[�o�[���ɐF��ς���
            Renderer.material.color = MouseOverColor;
        }
    }

    void OnMouseExit()
    {
        if (gamecontroller.gameState == "CanPush")
        {
            //�I���W�i���̐F�ɖ߂�
            Renderer.material.color = OriginalColor;
        }
    }
}
