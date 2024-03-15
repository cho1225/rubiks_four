using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private float x;
    private float z;
    private bool pushes;
    [SerializeField] Panel[] panels;

    public void SetPushes(bool _pushes)
    {
        this.pushes = _pushes;
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].ResetPanel();
        }

    }

    public (float, float) GetXZ() { return (x, z); }

    private void EnabledPanel(GameObject[,,] boardState)
    {
        foreach (Panel panel in panels)
        {
            if (boardState[(int)panel.GetX() + 1, 2, (int)panel.GetZ() + 1] != null)
            {
                panel.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                panel.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void EnabledAllPanel(string gameState, GameObject[,,] boardState)
    {
        if (gameState == "CanPush")
        {
            EnabledPanel(boardState);
        }
        else
        {
            foreach (Panel panel in panels)
            {
                panel.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public bool IsPushes()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].GetPush())
            {
                x = panels[i].GetX();
                z = panels[i].GetZ();
                pushes = true;
            }
        }

        return pushes;
    }
}
