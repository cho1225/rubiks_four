using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    // チェックする可能性のある方向
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
    // 勝利判定済みかどうか
    private bool hasJudge = false;

    // hasJudgeのプロパティ
    public bool HasJudge
    {
        get { return hasJudge; }
        set { hasJudge = value; }
    }

    // 勝者を判定
    public string CheckWinner(CubeFaller[,,] boardState)
    {
        if (!HasJudge)
        {
            foreach (Vector3 direction in directions)
            {
                // 各方向ごとに、ゲームボードをスキャンして勝者がいるかどうかを判定
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            if (boardState[i, j, k] != null)
                            {
                                // 現在のセルがプレイヤーの識別子と一致している場合、連続しているかどうかを確認
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

    // 指定された方向で3つの連続したセルが揃っているかどうかを確認
    private bool CheckDirection(int startX, int startY, int startZ, int player, Vector3 direction, CubeFaller[,,] boardState)
    {
        for (int i = 0; i < 3; i++)
        {
            int x = startX + (int)direction.x * i;
            int y = startY + (int)direction.y * i;
            int z = startZ + (int)direction.z * i;

            // ボードの範囲外に出た場合はfalseを返す
            if (x < 0 || x >= 3 || y < 0 || y >= 3 || z < 0 || z >= 3)
            {
                return false;
            }

            // 連続していない場合はfalseを返す
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
