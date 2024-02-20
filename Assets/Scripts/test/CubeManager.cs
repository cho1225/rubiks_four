using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public string cubeColor = "GrayCube";
    private float speed = 1.0f;

    private GameObject[,,] boardState = new GameObject[3, 3, 3];

    void Start()
    {
        GenerateCube(GenerateRandomPosition(), "GrayCube");
    }

    private (float, float) GenerateRandomPosition()
    {
        // ランダムなインデックスを選択
        int xRandomIndex = UnityEngine.Random.Range(-1, 2);
        int zRandomIndex = UnityEngine.Random.Range(-1, 2);

        return (xRandomIndex, zRandomIndex);
    }

    public void GenerateCube((float, float) position, string _cubeColor)
    {
        GameObject obj = (GameObject)Resources.Load(_cubeColor);
        // Cubeプレハブを元に、インスタンスを生成.
        GameObject newCube = Instantiate(obj);
        newCube.transform.parent = transform;
        newCube.transform.position = new Vector3(position.Item1, 1, position.Item2);
        SetBoardState(position, newCube);

        SwitchCubeColor();
    }

    private void SetBoardState((float, float) position, GameObject newCube)
    {
        for (int i = 0; i < 3; i++)
        {
            if (boardState[(int)position.Item1 + 1, i, (int)position.Item2 + 1] == null)
            {
                boardState[(int)position.Item1 + 1, i, (int)position.Item2 + 1] = newCube;
                return;
            }
        }
    }

    public GameObject[,,] GetBoardState() { return boardState; }

    private void SwitchCubeColor()
    {
        if (cubeColor == "RedCube")
        {
            this.cubeColor = "BlueCube";
        }
        else {
            this.cubeColor = "RedCube";
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
                        if (!boardState[i, j, k].GetComponent<Cube>().GetHasFalled())
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    public void MoveCube()
    {
        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                for(int k = 0; k < 3; k++)
                {
                    if (boardState[i, j, k] != null)
                    {
                        if (!CheckHasFalled(i, j, k))
                        {
                            boardState[i, j, k].transform.Translate(0, -speed * Time.deltaTime, 0, Space.World);
                        }
                        else
                        {
                            boardState[i, j, k].transform.position = new Vector3(i - 1, j - 1, k - 1);
                            boardState[i, j, k].GetComponent<Cube>().SetHasFalled(true);
                        }
                    }
                }
            }
        }
    }

    private bool CheckHasFalled(int i, int j, int k)
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
