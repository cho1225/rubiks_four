using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private float x;
    private float z;
    private bool pushes;
    [SerializeField] Panel[] panels;

    // ˆø”‚Í“n‚·‚×‚«H
    public void SetPushes()
    {
        this.pushes = false;
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].ResetPanel();
        }

    }

    public (float, float) XZ { get { return (x, z); } }

    public void EnabledAllPanel(string gameState, CubeFaller[,,] boardState)
    {
        if (gameState == "CanPush")
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

    public bool IsPushes()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].Push)
            {
                x = panels[i].X;
                z = panels[i].Z;
                pushes = true;
            }
        }
        return pushes;
    }
}
