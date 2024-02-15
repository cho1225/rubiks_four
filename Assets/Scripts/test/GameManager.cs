using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PanelManager panelManager;
    private CubeManager cubeManager;
    private string gameState = "CanPush";

    void Start()
    {
        // パネルマネージャーをとってくる
        GameObject allPanel = GameObject.Find("AllPanel");
        panelManager = allPanel.GetComponent<PanelManager>();

        // キューブマネージャーをとってくる
        GameObject allCube = GameObject.Find("AllCube");
        cubeManager = allCube.GetComponent<CubeManager>();
    }

    void Update()
    {
        if (gameState == "CanPush")
        {
            if (panelManager.IsPushes())
            {
                SetGameState("Falling");
                panelManager.SetPushes(false);
                cubeManager.GenerateCube(panelManager.GetXZ());
            }
            panelManager.EnabledAllPanel(gameState);
        }

        if (gameState == "Falling")
        {
            cubeManager.MoveCube();
            if (cubeManager.AllHasFalled())
            {
                SetGameState("Rotate");
            }
        }

        if (gameState == "Rotate")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetGameState("CanPush");
            }
        }
    }

    void SetGameState(string _gameState)
    {
        gameState = _gameState;
    }
}
