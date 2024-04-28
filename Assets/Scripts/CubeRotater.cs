// キューブの回転を制御
using System.Collections;
using UnityEngine;

public class CubeRotater : MonoBehaviour
{
    // 回転ボタンが押されたかどうか
    private bool isRotated = false;
    // 回転の方向のEnum
    public enum Direction
    {
        None,
        X,
        Z,
        REX,
        REZ
    }
    // ひとつ前に回転した方向
    private Direction preRotate = Direction.None;
    // 回転方向
    private Direction rotateDirection = Direction.None;
    // 回転中かどうか
    private bool rotationCheck = false;
    // 回転済みかどうか
    private bool hasRotated = false;
    // 回転速度
    private float speed = 1.0f;

    //------------各プロパティ

    public bool IsRotated 
    {
        get { return isRotated; }
        set { isRotated = value; }
    }

    public Direction PreRotate { get { return preRotate; } }

    public Direction RotateDirection { get { return this.rotateDirection; } }

    public bool HasRotated 
    { 
        get { return hasRotated; } 
        set { hasRotated = value; }
    }

    //------------

    // 回転方向をセットする（ボタンではEnumを引数として設定できなかったので別で用意した）
    public void SetRotateDirection(int directionIndex)
    {
        switch (directionIndex)
        {
            case 1:
                rotateDirection = Direction.X;
                break;
            case 2:
                rotateDirection = Direction.Z;
                break;
            case 3:
                rotateDirection = Direction.REX;
                break;
            case 4:
                rotateDirection = Direction.REZ;
                break;
            default:
                rotateDirection = Direction.None;
                break;
        }
    }

    // キューブの回転処理
    public CubeFaller[,,] RotateCube(Direction direction, CubeFaller[,,] boardState)
    {
        preRotate = direction;
        isRotated = true;

        if (rotationCheck == false)
        {
            rotationCheck = true;
            StartCoroutine(Rotate(direction, boardState));
        }

        return RotateBoardState(direction, boardState);
    }

    // 回転処理
    IEnumerator Rotate(Direction direction, CubeFaller[,,] boardState)
    {
        float count = 90 / speed;

        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.01f);
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for(int l = 0; l < 3; l++)
                    {
                        if (boardState[j, k, l] != null)
                        {
                            switch (direction)
                            {
                                case Direction.X:
                                    boardState[j, k, l].transform.RotateAround(new Vector3(0, 0, 0), Vector3.left, speed);
                                    break;
                                case Direction.Z:
                                    boardState[j, k, l].transform.RotateAround(new Vector3(0, 0, 0), Vector3.back, speed);
                                    break;
                                case Direction.REX:
                                    boardState[j, k, l].transform.RotateAround(new Vector3(0, 0, 0), Vector3.right, speed);
                                    break;
                                case Direction.REZ:
                                    boardState[j, k, l].transform.RotateAround(new Vector3(0, 0, 0), Vector3.forward, speed);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }
        rotationCheck = false;
        hasRotated = true;
    }

    // 配列の回転処理
    private CubeFaller[,,] RotateBoardState(Direction direction, CubeFaller[,,] boardState)
    {
        CubeFaller[,,] rotatedBoardState = new CubeFaller[3, 3, 3];

        // 配列を回転
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    switch (direction)
                    {
                        case Direction.X:
                            rotatedBoardState[i, k, -j + 2] = boardState[i, j, k];
                            break;
                        case Direction.Z:
                            rotatedBoardState[j, -i + 2, k] = boardState[i, j, k];
                            break;
                        case Direction.REX:
                            rotatedBoardState[i, -k + 2, j] = boardState[i, j, k];
                            break;
                        case Direction.REZ:
                            rotatedBoardState[-j + 2, i, k] = boardState[i, j, k];
                            break;
                        default:
                            rotatedBoardState[i, j, k] = boardState[i, j, k];
                            break;
                    }
                }
            }
        }

        //回転後の判定の配列の落下
        for (int i = 0; i < 3; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                // 2段目の処理
                if (rotatedBoardState[i, 1, k] != null && rotatedBoardState[i, 0, k] == null)
                {
                    rotatedBoardState[i, 0, k] = rotatedBoardState[i, 1, k];
                    rotatedBoardState[i, 1, k] = null;
                }

                // 3段目の処理
                if (rotatedBoardState[i, 2, k] != null && rotatedBoardState[i, 1, k] == null)
                {
                    rotatedBoardState[i, 1, k] = rotatedBoardState[i, 2, k];
                    rotatedBoardState[i, 2, k] = null;
                    if (rotatedBoardState[i, 1, k] != null && rotatedBoardState[i, 0, k] == null)
                    {
                        rotatedBoardState[i, 0, k] = rotatedBoardState[i, 1, k];
                        rotatedBoardState[i, 1, k] = null;
                    }
                }
            }
        }

        return rotatedBoardState;
    }
}
