﻿// パネル全体を制御
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    // 参照するスクリプト
    [SerializeField] Panel[] panels;
    // 押されたパネルのx座標を保持する変数
    private float x;
    // 押されたパネルのz座標を保持する変数
    private float z;
    // パネルがどれか一つでも押されたかどうか
    private bool pushes;

    // xとzのプロパティ
    public (float, float) XZ { get { return (x, z); } }

    // Panelを一括で初期化
    public void InitializePanels()
    {
        foreach (Panel panel in panels)
        {
            panel.InitializePanel();
        }
    }

    // pushesと各パネルのpushをfalseに設定
    public void SetPushes()
    {
        this.pushes = false;
        foreach (Panel panel in panels)
        {
            panel.ResetPanel();
        }

    }

    // すべてのパネルのコライダーが有効かどうかを管理
    public void EnabledAllPanel(GameManager.GameState gameState, CubeFaller[,,] boardState)
    {
        if (gameState == GameManager.GameState.CanPush)
        {
            foreach (Panel panel in panels)
            {
                if (boardState[(int)panel.X + 1, 2, (int)panel.Z + 1] != null)
                {
                    panel.SetEnabledPanel(false);
                }
                else
                {
                    panel.SetEnabledPanel(true);
                }
            }
        }
        else
        {
            foreach (Panel panel in panels)
            {
                panel.SetEnabledPanel(false);
            }
        }
    }

    // パネルがどれか一つでも押されたかどうかを判定
    public bool IsPushes()
    {
        foreach (Panel panel in panels)
        {
            if (panel.Push)
            {
                x = panel.X;
                z = panel.Z;
                pushes = true;
            }
        }
        return pushes;
    }
}
