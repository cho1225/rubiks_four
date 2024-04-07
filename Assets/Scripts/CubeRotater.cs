using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeRotater : MonoBehaviour
{
    // ‰ñ“]ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½‚©‚Ç‚¤‚©
    private bool isRotated = false;
    // ‚Ğ‚Æ‚Â‘O‚É‰ñ“]‚µ‚½•ûŒü
    private string preRotate = "none";
    // ‰ñ“]•ûŒü
    private string direction;
    // ‰ñ“]’†‚©‚Ç‚¤‚©
    private bool rotationCheck = false;
    // ‰ñ“]Ï‚İ‚©‚Ç‚¤‚©
    private bool hasRotated = false;
    // ‰ñ“]‘¬“x
    private float speed = 1;

    //------------ŠeƒvƒƒpƒeƒB

    public bool IsRotated 
    {
        get { return isRotated; }
        set { isRotated = value; }
    }

    public string PreRotate { get { return preRotate; } }

    public string Direction
    {
        get { return this.direction; }
        set { direction = value; }
    }

    public bool HasRotated 
    { 
        get { return hasRotated; } 
        set { hasRotated = value; }
    }

    //------------

    // ƒLƒ…[ƒu‚Ì‰ñ“]ˆ—
    public CubeFaller[,,] RotateCube(string _direction, CubeFaller[,,] boardState)
    {
        preRotate = _direction;
        isRotated = true;

        if (rotationCheck == false)
        {
            rotationCheck = true;
            StartCoroutine(Rotate(_direction, boardState));
        }

        return RotateBoardState(_direction, boardState);
    }

    // ‰ñ“]ˆ—
    IEnumerator Rotate(string _direction, CubeFaller[,,] boardState)
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
                            switch (_direction)
                            {
                                case "x":
                                    boardState[j, k, l].transform.RotateAround(new Vector3(0, 0, 0), Vector3.left, speed);
                                    break;
                                case "z":
                                    boardState[j, k, l].transform.RotateAround(new Vector3(0, 0, 0), Vector3.back, speed);
                                    break;
                                case "rex":
                                    boardState[j, k, l].transform.RotateAround(new Vector3(0, 0, 0), Vector3.right, speed);
                                    break;
                                case "rez":
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

    // ”z—ñ‚Ì‰ñ“]ˆ—
    private CubeFaller[,,] RotateBoardState(string _direction, CubeFaller[,,] boardState)
    {
        CubeFaller[,,] rotatedBoardState = new CubeFaller[3, 3, 3];

        // ”z—ñ‚ğ‰ñ“]
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    switch (_direction)
                    {
                        case "x":
                            rotatedBoardState[i, k, -j + 2] = boardState[i, j, k];
                            break;
                        case "z":
                            rotatedBoardState[j, -i + 2, k] = boardState[i, j, k];
                            break;
                        case "rex":
                            rotatedBoardState[i, -k + 2, j] = boardState[i, j, k];
                            break;
                        case "rez":
                            rotatedBoardState[-j + 2, i, k] = boardState[i, j, k];
                            break;
                        default:
                            rotatedBoardState[i, j, k] = boardState[i, j, k];
                            break;
                    }
                }
            }
        }

        //‰ñ“]Œã‚Ì”»’è‚Ì”z—ñ‚Ì—‰º
        for (int i = 0; i < 3; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                // 2’i–Ú‚Ìˆ—
                if (rotatedBoardState[i, 1, k] != null && rotatedBoardState[i, 0, k] == null)
                {
                    rotatedBoardState[i, 0, k] = rotatedBoardState[i, 1, k];
                    rotatedBoardState[i, 1, k] = null;
                }

                // 3’i–Ú‚Ìˆ—
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
