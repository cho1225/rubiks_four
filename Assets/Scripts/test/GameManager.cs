using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PanelManager panelManager;
    private string gameState = "CanPush";

    void Start()
    {
        GameObject allPanel = GameObject.Find("AllPanel");
        panelManager = allPanel.GetComponent<PanelManager>();
    }

    void Update()
    {
        if (gameState == "CanPush")
        {
            if (panelManager.IsPushes())
            {
                SetGameStateCanPush("Rotate");
                panelManager.SetPushes(false);
                (float, float) result = panelManager.GetXZ();
            }
            panelManager.EnabledAllPanel(gameState);
        }


        if (Input.GetKey(KeyCode.W))
        {
            SetGameStateCanPush("CanPush");
        }
    }

    void SetGameStateCanPush(string _gameState)
    {
        gameState = _gameState;
    }
}
