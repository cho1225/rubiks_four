using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeFaller : MonoBehaviour
{
    // ���̃L���[�u�������ς݂�
    private bool hasFalled = false;
    // �����̃X�s�[�h
    private float speed = 1.0f;
    /* ���̃L���[�u�����F��
       �D�F��0�A�ԐF��1�A�F��2 */
    [SerializeField] private int cubeColorIndex;

    //------------�e�v���p�e�B

    public bool HasFalled
    {
        get { return hasFalled; }
        set { hasFalled = value; }
    }

    public int CubeColorIndex { get { return cubeColorIndex; } }

    //------------

    // �L���[�u�̗�������
    public void FallCube()
    {
        this.transform.Translate(0, -speed * Time.deltaTime, 0, Space.World);
    }

    // �L���[�u�������ς݂��ǂ����𔻒�
    public bool CheckHasFalled(CubeFaller[,,] boardState, int i, int j, int k)
    {
        if (j == 0)
        {
            if (boardState[i, j, k].transform.position.y > -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (boardState[i, j, k].transform.position.y - 1 > boardState[i, j - 1, k].transform.position.y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
