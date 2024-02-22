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
    private bool hasJudge = false;

    public void SetHasJudge()
    {
        hasJudge = false;
    }

    public string CheckWinner(GameObject[,,] boardState)
    {
        if (!hasJudge)
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
                                if (boardState[i, j, k].CompareTag("Red"))
                                {
                                    if (CheckDirection(i, j, k, "Red", direction, boardState))
                                    {
                                        return "red";
                                    }
                                }
                                if (boardState[i, j, k].CompareTag("Blue"))
                                {
                                    if (CheckDirection(i, j, k, "Blue", direction, boardState))
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
        hasJudge = true;
        return "done";
    }

    // 特定の方向で3つの連続したセルが揃っているかどうかを確認
    private bool CheckDirection(int startX, int startY, int startZ, string player, Vector3 direction, GameObject[,,] boardState)
    {
        for (int i = 0; i < 3; i++)
        {
            int x = startX + (int)direction.x * i;
            int y = startY + (int)direction.y * i;
            int z = startZ + (int)direction.z * i;

            // ボードの範囲外に出た場合は false を返す
            if (x < 0 || x >= 3 || y < 0 || y >= 3 || z < 0 || z >= 3)
            {
                return false;
            }

            // 連続していない場合は false を返す
            if (boardState[x, y, z] == null)
            {
                return false;
            }
            else
            {
                if (!boardState[x, y, z].CompareTag(player))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
