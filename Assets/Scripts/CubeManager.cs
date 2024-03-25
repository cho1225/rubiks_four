using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private CubeFaller[,,] boardState = new CubeFaller[3, 3, 3];
    [SerializeField] private CubeRotater cubeRotater;
    [SerializeField] private GameObject[] Cubes;

    private int nextCubeColorIndex = 0;

    void Start()
    {
        GenerateCube(GenerateRandomPosition(), nextCubeColorIndex);
    }

    public CubeFaller[,,] BoardState
    {
        get { return boardState; }
        set { boardState = value; }
    }

    private (float, float) GenerateRandomPosition()
    {
        // ランダムなインデックスを選択
        int xRandomIndex = UnityEngine.Random.Range(-1, 2);
        int zRandomIndex = UnityEngine.Random.Range(-1, 2);

        return (xRandomIndex, zRandomIndex);
    }

    public void GenerateCube((float, float) position, int _cubeColor)
    {
        GameObject obj = Cubes[_cubeColor];
        GameObject newCube = Instantiate(obj);
        newCube.transform.parent = transform;
        newCube.transform.position = new Vector3(position.Item1, 1, position.Item2);
        SetCubeOnBoard(position, newCube);

        SwitchCubeColor();
    }

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

    public int NextCubeColorIndex { get { return nextCubeColorIndex; } }

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

    public CubeRotater CubeRotater { get { return cubeRotater; } }
}
