using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    //------------�Q�Ƃ���X�N���v�g

    private CubeFaller[,,] boardState = new CubeFaller[3, 3, 3];
    [SerializeField] private CubeRotater cubeRotater;

    //------------

    // �L���[�u�����̂��߂̃v���n�u
    [SerializeField] private GameObject[] Cubes;
    /* ���ɐ��������L���[�u�����F��
       �D�F��0�A�ԐF��1�A�F��2 */
    private int nextCubeColorIndex = 0;

    // �ŏ��Ƀ����_���ȏꏊ�ɊD�F�̃L���[�u�𐶐�����
    void Start()
    {
        GenerateCube(GenerateRandomPosition(), nextCubeColorIndex);
    }

    //------------�e�v���p�e�B

    public CubeFaller[,,] BoardState
    {
        get { return boardState; }
        set { boardState = value; }
    }

    public int NextCubeColorIndex { get { return nextCubeColorIndex; } }

    //------------

    // �����_���ȍ��W���쐬
    private (float, float) GenerateRandomPosition()
    {
        // -1�`1�܂ł̃����_���ȃC���f�b�N�X����
        int xRandomIndex = UnityEngine.Random.Range(-1, 2);
        int zRandomIndex = UnityEngine.Random.Range(-1, 2);

        return (xRandomIndex, zRandomIndex);
    }

    // �w�肳�ꂽ���W�ɔC�ӂ̃L���[�u�𐶐�
    public void GenerateCube((float, float) position, int _cubeColor)
    {
        GameObject obj = Cubes[_cubeColor];
        GameObject newCube = Instantiate(obj);
        newCube.transform.parent = transform;
        newCube.transform.position = new Vector3(position.Item1, 1, position.Item2);
        
        SetCubeOnBoard(position, newCube);

        SwitchCubeColor();
    }

    // �������ꂽ�L���[�u�i�����ρj��z��ɂ��炩���ߒǉ�
    private void SetCubeOnBoard((float, float) position, GameObject newCube)
    {
        for (int i = 0; i < 3; i++)
        {
            if (boardState[(int)position.Item1 + 1, i, (int)position.Item2 + 1] == null)
            {
                boardState[(int)position.Item1 + 1, i, (int)position.Item2 + 1] = newCube.GetComponent<CubeFaller>();
                return;
            }
        }
    }

    // ���ɐ��������L���[�u�̐F��؂�ւ���
    private void SwitchCubeColor()
    {
        if (nextCubeColorIndex == 1)
        {
            this.nextCubeColorIndex = 2;
        }
        else {
            this.nextCubeColorIndex = 1;
        }
    }

    // ���ׂẴL���[�u�̗����������Ǘ�
    public void FallAllCube()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (boardState[i, j, k] != null)
                    {
                        if (!boardState[i, j, k].CheckHasFalled(BoardState, i, j, k))
                        {
                            boardState[i, j, k].FallCube();
                        }
                        else
                        {
                            boardState[i, j, k].transform.position = new Vector3(i - 1, j - 1, k - 1);
                            boardState[i, j, k].HasFalled = true;
                        }
                    }
                }
            }
        }
    }

    // ���ׂẴL���[�u�������ς݂��ǂ����𔻒�
    public bool AllHasFalled()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (boardState[i, j, k] != null)
                    {
                        if (!boardState[i, j, k].HasFalled)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    // ���ׂẴL���[�u�𖢗����ɐݒ�
    public void ResetAllCube()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (boardState[i, j, k] != null)
                    {
                        boardState[i, j, k].HasFalled = false;
                    }
                }
            }
        }
    }

    // CubeRotater��Ԃ�
    public CubeRotater CubeRotater { get { return cubeRotater; } }
}
