using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Result : MonoBehaviour
{
    private int winner;
    private int[,,] resultBoardState = new int[3, 3, 3];

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Result");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void InitializeResult()
    {
        winner = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    resultBoardState[i, j, k] = 9;
                }
            }
        }
    }

    public void SetResult(CubeFaller[,,] boardState, string judge)
    {
        if (judge == "red")
        {
            winner = 1;
        }
        else if (judge == "blue")
        {
            winner = 2;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (boardState[i, j, k])
                    {
                        if (boardState[i, j, k].CubeColorIndex == 1)
                        {
                            resultBoardState[i, j, k] = 1;
                        }
                        else if (boardState[i, j, k].CubeColorIndex == 2)
                        {
                            resultBoardState[i, j, k] = 2;
                        }
                        else if (boardState[i, j, k].CubeColorIndex == 0)
                        {
                            resultBoardState[i, j, k] = 0;
                        }
                    }
                }
            }
        }
    }

    public int[,,] GetResultBoardState() { return resultBoardState; }

    public int GetWinner() { return winner; }
}
