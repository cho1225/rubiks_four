using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    // �`�F�b�N����\���̂������
    private Vector3[] directions = {
        Vector3.right,
        Vector3.up,
        Vector3.forward,
        new Vector3(1, 1, 0),
        new Vector3(-1, 1, 0),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, 1),
        new Vector3(0, 1, 1),
        new Vector3(0, 1, -1),
        new Vector3(1, 1, 1),
        new Vector3(-1, 1, 1),
        new Vector3(-1, 1, -1),
        new Vector3(1, -1, -1) 
    };
    // ��������ς݂��ǂ���
    private bool hasJudge = false;

    // hasJudge�̃v���p�e�B
    public bool HasJudge
    {
        get { return hasJudge; }
        set { hasJudge = value; }
    }

    // ���҂𔻒�
    public string CheckWinner(CubeFaller[,,] boardState)
    {
        if (!HasJudge)
        {
            foreach (Vector3 direction in directions)
            {
                // �e�������ƂɁA�Q�[���{�[�h���X�L�������ď��҂����邩�ǂ����𔻒�
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            if (boardState[i, j, k] != null)
                            {
                                // ���݂̃Z�����v���C���[�̎��ʎq�ƈ�v���Ă���ꍇ�A�A�����Ă��邩�ǂ������m�F
                                if (boardState[i, j, k].CubeColorIndex == 1)
                                {
                                    if (CheckDirection(i, j, k, 1, direction, boardState))
                                    {
                                        return "red";
                                    }
                                }
                                if (boardState[i, j, k].CubeColorIndex == 2)
                                {
                                    if (CheckDirection(i, j, k, 2, direction, boardState))
                                    {
                                        return "blue";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        HasJudge = true;
        return "done";
    }

    // �w�肳�ꂽ������3�̘A�������Z���������Ă��邩�ǂ������m�F
    private bool CheckDirection(int startX, int startY, int startZ, int player, Vector3 direction, CubeFaller[,,] boardState)
    {
        for (int i = 0; i < 3; i++)
        {
            int x = startX + (int)direction.x * i;
            int y = startY + (int)direction.y * i;
            int z = startZ + (int)direction.z * i;

            // �{�[�h�͈̔͊O�ɏo���ꍇ��false��Ԃ�
            if (x < 0 || x >= 3 || y < 0 || y >= 3 || z < 0 || z >= 3)
            {
                return false;
            }

            // �A�����Ă��Ȃ��ꍇ��false��Ԃ�
            if (boardState[x, y, z] == null)
            {
                return false;
            }
            else
            {
                if (boardState[x, y, z].CubeColorIndex != player)
                {
                    return false;
                }
            }
        }

        return true;
    }
}