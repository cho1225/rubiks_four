using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateManager : MonoBehaviour
{
    private bool hasRotated = false;
    private string preRotate = "no";
    public string direction = "no";
    private bool rotationCheck = false;
    [SerializeField] private bool allHasRotate = false;

    private float count;
    private float speed = 1;

    public void SetHasRotated(bool _hasRotated) { hasRotated = _hasRotated; }

    public bool GetHasRotated() { return hasRotated; }

    public string GetPreRotate() { return preRotate; }

    public void SetDirection(string _direction) {  direction = _direction; }

    public string GetDirection() { return direction; }

    public void SetAllHasRotate(bool _bool) { allHasRotate = _bool; }

    public bool GetAllHasRotate() { return allHasRotate; } 

    public GameObject[,,] Rotate(string _direction, GameObject[,,] boardState)
    {
        preRotate = _direction;
        hasRotated = true;

        if (rotationCheck == false)
        {
            rotationCheck = true;
            StartCoroutine(Delay(_direction, boardState));
        }

        return RotateBoardState(_direction, boardState);
    }

    IEnumerator Delay(string _direction, GameObject[,,] boardState)
    {
        count = 90 / speed;

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
        allHasRotate = true;
    }

    private GameObject[,,] RotateBoardState(string _direction, GameObject[,,] boardState)
    {
        GameObject[,,] rotatedBoardState = new GameObject[3, 3, 3];

        //”»’è‚Ì”z—ñ‚ð‰ñ“]
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

        //‰ñ“]Œã‚Ì”»’è‚Ì”z—ñ‚Ì—Ž‰º
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
