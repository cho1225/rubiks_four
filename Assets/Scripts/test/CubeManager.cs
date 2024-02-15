using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private string cubeColor = "RedCube";
    private float speed = 1.0f;

    public GameObject[,,] boardState = new GameObject[3, 3, 3];

    void Start()
    {

    }

    public void GenerateCube((float, float) position)
    {
        GameObject obj = (GameObject)Resources.Load(cubeColor);
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
                        boardState[i, j, k].transform.Translate(0, -speed * Time.deltaTime, 0, Space.World);
                    }
                }
            }
        }
    }
}
