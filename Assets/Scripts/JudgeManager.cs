// 勝ち負けの判定を制御
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
    // 勝者のEnum
    public enum Winner
    {
        None,
        Red,
        Blue,
        Draw
    }

    // hasJudgeのプロパティ
    public bool HasJudge
    {
        get { return hasJudge; }
        set { hasJudge = value; }
    }

    // 勝者を判定
    public Winner CheckWinner(CubeFaller[,,] boardState)
    {
        // 赤がそろっている数
        int redWinCount = 0;
        // 青がそろっている数
        int blueWinCount = 0;

        if (!HasJudge)
        {
            foreach (Vector3 direction in directions)
            {
                // 各方向ごとに、盤面をスキャンして勝者がいるかどうかを判定
                for (int i = 0; i < boardState.GetLength(0); i++)
                {
                    for (int j = 0; j < boardState.GetLength(1); j++)
                    {
                        for (int k = 0; k < boardState.GetLength(2); k++)
                        {
                            if (boardState[i, j, k] != null)
                            {
                                // 現在のセルがプレイヤーの識別子と一致している場合、連続しているかどうかを確認
                                if (boardState[i, j, k].GetCubeColor == CubeManager.CubeColor.Red)
                                {
                                    if (CheckDirection(i, j, k, CubeManager.CubeColor.Red, direction, boardState))
                                    {
                                        redWinCount++;
                                    }
                                }
                                if (boardState[i, j, k].GetCubeColor == CubeManager.CubeColor.Blue)
                                {
                                    if (CheckDirection(i, j, k, CubeManager.CubeColor.Blue, direction, boardState))
                                    {
                                        blueWinCount++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // そろっている数で勝者を判定
            if (redWinCount > blueWinCount)
            {
                return Winner.Red;
            }
            else if (blueWinCount > redWinCount)
            {
                return Winner.Blue;
            }

            // 盤面が埋まったら引き分け
            if (CheckFullBoard(boardState))
            {
                return Winner.Draw;
            }

            HasJudge = true;
        }
        return Winner.None;
    }

    // 指定された方向で3つの連続したセルが揃っているかどうかを判定
    private bool CheckDirection(int startX, int startY, int startZ, CubeManager.CubeColor player, Vector3 direction, CubeFaller[,,] boardState)
    {
        for (int i = 0; i < 3; i++)
        {
            // チェックしたいセルの場所
            int x = startX + (int)direction.x * i;
            int y = startY + (int)direction.y * i;
            int z = startZ + (int)direction.z * i;

            // 盤面の範囲外に出た場合はfalseを返す
            if (x < 0 || x >= boardState.GetLength(0) || y < 0 || y >= boardState.GetLength(1) || z < 0 || z >= boardState.GetLength(2))
            {
                return false;
            }

            // 連続していない場合はfalseを返す
            if (boardState[x, y, z] == null || boardState[x, y, z].GetCubeColor != player)
            {
                return false;
            }
        }

        return true;
    }

    // 盤面が完全に埋まっているかどうかを判定
    private bool CheckFullBoard(CubeFaller[,,] boardState)
    {
        int cubeCount = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (boardState[i, j, k])
                    {
                        cubeCount++;
                    }
                }
            }
        }

        if (cubeCount == 27)
        {
            return true;
        }

        return false;
    }
}
