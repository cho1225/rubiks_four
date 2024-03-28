using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    // �Q�Ƃ���X�N���v�g
    [SerializeField] Panel[] panels;
    // �����ꂽ�p�l����x���W��ێ�����ϐ�
    private float x;
    // �����ꂽ�p�l����z���W��ێ�����ϐ�
    private float z;
    // �p�l�����ǂꂩ��ł������ꂽ���ǂ���
    private bool pushes;

    // x��z�̃v���p�e�B
    public (float, float) XZ { get { return (x, z); } }

    // pushes�Ɗe�p�l����push��false�ɐݒ�
    public void SetPushes()
    {
        this.pushes = false;
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].ResetPanel();
        }

    }

    // ���ׂẴp�l���̃R���C�_�[���L�����ǂ������Ǘ�
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

    // �p�l�����ǂꂩ��ł������ꂽ���ǂ����𔻒�
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
