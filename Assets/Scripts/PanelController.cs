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
    public int[,,] panelBoardState = new int[3, 3, 3];

    void Start()
    {
        //Renderer�R���|�[�l���g���Ƃ��Ă���
        Renderer = GetComponent<MeshRenderer>();
        //�I���W�i���̐F���Ƃ��Ă���
        OriginalColor = Renderer.material.color;

        GameObject gameObject = GameObject.Find("GameObject");
        gamecontroller = gameObject.GetComponent<GameController>();
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
            if (panelBoardState[PanelNumberX, PanelNumberY, 2] == 0)
            {
                // Cube�v���n�u��GameObject�^�Ŏ擾
                if (gamecontroller.cubeColorState == "Red")
                {
                    GameObject obj = (GameObject)Resources.Load("RedCube");
                    // Cube�v���n�u�����ɁA�C���X�^���X�𐶐��A
                    GameObject newCube = Instantiate(obj, parentObject.transform);
                    newCube.transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
                    gamecontroller.spawnedCubes.Add(newCube);
                    if (panelBoardState[PanelNumberX, PanelNumberY, 0] == 0)
                    {
                        panelBoardState[PanelNumberX, PanelNumberY, 0] = 1;
                    }
                    else if (panelBoardState[PanelNumberX, PanelNumberY, 1] == 0)
                    {
                        panelBoardState[PanelNumberX, PanelNumberY, 1] = 1;
                    }
                    else
                    {
                        panelBoardState[PanelNumberX, PanelNumberY, 2] = 1;
                    }
                    gamecontroller.cubeColorState = "Blue";
                }
                else if (gamecontroller.cubeColorState == "Blue")
                {
                    GameObject obj = (GameObject)Resources.Load("BlueCube");
                    // Cube�v���n�u�����ɁA�C���X�^���X�𐶐��A
                    GameObject newCube = Instantiate(obj, parentObject.transform);
                    newCube.transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
                    gamecontroller.spawnedCubes.Add(newCube);
                    if (panelBoardState[PanelNumberX, PanelNumberY, 0] == 0)
                    {
                        panelBoardState[PanelNumberX, PanelNumberY, 0] = 2;
                    }
                    else if (panelBoardState[PanelNumberX, PanelNumberY, 1] == 0)
                    {
                        panelBoardState[PanelNumberX, PanelNumberY, 1] = 2;
                    }
                    else
                    {
                        panelBoardState[PanelNumberX, PanelNumberY, 2] = 2;
                    }
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
