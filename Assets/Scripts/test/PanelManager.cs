using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private float x;
    private float z;
    private bool pushes;
    Panel[] panels;

    void Start()
    {
        // パネルの情報をセット
        panels = GetComponentsInChildren<Panel>();
    }

    public void SetPushes(bool _pushes)
    {
        this.pushes = _pushes;
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].ResetPanel();
        }

    }

    public (float, float) GetXZ() { return (x, z); }

    public void EnabledAllPanel(string gameState)
    {
        if (gameState == "CanPush")
        {
            foreach (Panel panel in panels)
            {
                panel.GetComponent<BoxCollider>().enabled = true;
            }
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
