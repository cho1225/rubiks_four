using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject canvas;

    private PanelManager panelManager;
    private CubeManager cubeManager;
    private UIManager uiManager;
    private RotateManager rotateManager;

    private string[] gameState = { "CanPush", "Falling", "Judge", "Rotate", "Falling", "Judge" };
    [SerializeField]private int gameStateNumber = 4;

    void Start()
    {
        // PanelManager‚ð‚Æ‚Á‚Ä‚­‚é
        GameObject allPanel = GameObject.Find("AllPanel");
        panelManager = allPanel.GetComponent<PanelManager>();

        // CubeManager‚ð‚Æ‚Á‚Ä‚­‚é
        GameObject allCube = GameObject.Find("AllCube");
        cubeManager = allCube.GetComponent<CubeManager>();

        // UIManager‚ð‚Æ‚Á‚Ä‚­‚é
        GameObject uiManagerObj = GameObject.Find("UIManager");
        uiManager = uiManagerObj.GetComponent<UIManager>();

        // RotateManager‚ð‚Æ‚Á‚Ä‚­‚é
        GameObject rotateManagerObj = GameObject.Find("RotateManager");
        rotateManager = rotateManagerObj.GetComponent<RotateManager>();
    }

    void Update()
    {
        if (gameState[gameStateNumber] == "CanPush")
        {
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetGameState();
            }
        }
    }

    void SetGameState()
    {
        gameStateNumber = (gameStateNumber + 1) % 6;
    }
}
