using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    //マウスオーバー時の色
    private Color MouseOverColor = Color.gray;

    //オリジナルの色
    private Color OriginalColor;

    //ゲームオブジェクトのRenderer
    MeshRenderer Renderer;

    public GameObject parentObject;

    GameController gamecontroller;
    public int PanelNumberX;
    public int PanelNumberY;
    public int[,,] panelBoardState;
    private bool onBoard = false;

    void Start()
    {
        //Rendererコンポーネントをとってくる
        Renderer = GetComponent<MeshRenderer>();
        //オリジナルの色をとってくる
        OriginalColor = Renderer.material.color;

        GameObject gameObject = GameObject.Find("GameObject");
        gamecontroller = gameObject.GetComponent<GameController>();
        panelBoardState = new int[gamecontroller.boardSize, gamecontroller.boardSize, gamecontroller.boardSize];
    }

    public void PanelUpdate()
    {
        if (gamecontroller.gameState != "CanPush")
        {
            //オリジナルの色に戻す
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
                // CubeプレハブをGameObject型で取得
                if (gamecontroller.cubeColorState == "Red")
                {
                    GameObject obj = (GameObject)Resources.Load("RedCube");
                    // Cubeプレハブを元に、インスタンスを生成、
                    GameObject newCube = Instantiate(obj, parentObject.transform);
                    newCube.transform.position = new Vector3(transform.position.x, (gamecontroller.boardSize - 1) / 2, transform.position.z);
                    gamecontroller.spawnedCubes.Add(newCube);

                    CheckOnBoard(1);

                    gamecontroller.cubeColorState = "Blue";
                }
                else if (gamecontroller.cubeColorState == "Blue")
                {
                    GameObject obj = (GameObject)Resources.Load("BlueCube");
                    // Cubeプレハブを元に、インスタンスを生成、
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
            //マウスオーバー時に色を変える
            Renderer.material.color = MouseOverColor;
        }
    }

    void OnMouseExit()
    {
        if (gamecontroller.gameState == "CanPush")
        {
            //オリジナルの色に戻す
            Renderer.material.color = OriginalColor;
        }
    }
}
