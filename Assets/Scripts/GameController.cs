using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject canvas;
    public GameObject pareObj;
    public GameObject buttonX;
    public GameObject buttonZ;
    public GameObject buttonReX;
    public GameObject buttonReZ;
    public GameObject waku;
    public static string winner = "";
    public int boardSize = 3;
    public int[,,] boardState = new int[3, 3, 3];
    public int[,,] rotatedBoardState = new int[3, 3, 3];
    public static int[,,] resultBoardState = new int[3, 3, 3];

    public List<GameObject> spawnedCubes = new List<GameObject>();
    public List<GameObject> panels = new List<GameObject>();
    int temp;
    float tempX;
    float tempY;
    float tempZ;
    Vector3 centerPoint = Vector3.zero;

    CubeController cubecontroller;
    PanelController panelcontroller;
    ChangeScene changescene;

    public string rotateState = "NoRotate";
    public string cubeColorState = "Red";
    public string gameState = "CanPush";
    public bool hasPush = false;
    bool hasRotate = false;
    public bool hasJudge = true;
    public string preRoate = "";
    public bool RotationCheck = false;
    public float Speed = 0.002f;
    public float Count;
    public bool activeCanvas = false;

    public bool rePosition = false;

    void Start()
    {
        changescene = this.GetComponent<ChangeScene>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    boardState[i, j, k] = 0;
                    rotatedBoardState[i, j, k] = 0;
                }
            }
        }

        

        GenerateRandomCube();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i =0; i < 9; i++)
        {
            panelcontroller = panels[i].GetComponent<PanelController>();
            panelcontroller.PanelUpdate();
        }

        for (int i = 0;i < spawnedCubes.Count; i++)
        {
            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();
            cubecontroller.CubeStart();
            cubecontroller.CubeUpdate();
        }

        if (gameState == "Rotate" && hasRotate != true)
        {
            if (!activeCanvas)
            {
                StartCoroutine(Delay());
                activeCanvas = true;
            }
        }
        else
        {
            canvas.SetActive(false);
        }

        CheckHasFalled();

        if (gameState == "Judge")
        {
            CheckWinner();
        }

        UnInteractiveButton(preRoate);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);
        canvas.SetActive(true);
    }

    void UnInteractiveButton(string preButton)
    {
        Button bt1 = buttonX.GetComponent<Button>();
        Button bt2 = buttonZ.GetComponent<Button>();
        Button bt3 = buttonReX.GetComponent<Button>();
        Button bt4 = buttonReZ.GetComponent<Button>();
        switch (preButton)
        {
            case "XRotate":
                bt1.interactable = true;
                bt2.interactable = true;
                bt3.interactable = false;
                bt4.interactable = true;
                break;
            case "ZRotate":
                bt1.interactable = true;
                bt2.interactable = true;
                bt3.interactable = true;
                bt4.interactable = false;
                break;
            case "ReXRotate":
                bt1.interactable = false;
                bt2.interactable = true;
                bt3.interactable = true;
                bt4.interactable = true;
                break;
            case "ReZRotate":
                bt1.interactable = true;
                bt2.interactable = false;
                bt3.interactable = true;
                bt4.interactable = true;
                break;
            default:
                bt1.interactable = true;
                bt2.interactable = true;
                bt3.interactable = true;
                bt4.interactable = true;
                break;
        }

    }

    void GenerateRandomCube()
    {
        // ランダムなインデックスを選択
        int xRandomIndex = Random.Range(0, 3);
        int zRandomIndex = Random.Range(0, 3);

        GameObject obj = (GameObject)Resources.Load("GrayCube");
        // Cubeプレハブを元に、インスタンスを生成、
        GameObject newCube = Instantiate(obj, pareObj.transform);
        newCube.transform.position = new Vector3(xRandomIndex - 1, 1, zRandomIndex - 1);
        spawnedCubes.Add(newCube);
        boardState[(int)newCube.transform.position.x + 1, (int)newCube.transform.position.z + 1, 0] = 3;
    }

    public void XRotate()
    {
        for (int i  = 0; i < panels.Count; i++)
        {
            panels[i].GetComponent<BoxCollider>().enabled = false;
        }

        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();
            cubecontroller.canCollision = false;
            spawnedCubes[i].GetComponent<BoxCollider>().enabled = false;
            waku.SetActive(false);
        }

        if (RotationCheck == false)
        {
            RotationCheck = true;
            rePosition = false;
            StartCoroutine("DelayX");
        }

        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();
            cubecontroller.cubePhsicsState = "falling";
        }

        //判定の配列を回転
        for (int i = 0;i < 3; i++)
        {
            for (int j = 0;j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    rotatedBoardState[i,-k+2,j] = boardState[i,j,k];
                }
            }
        }

        //回転後の判定の配列の落下
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // 2段目の処理
                if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                {
                    rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                    rotatedBoardState[i, j, 1] = 0;
                }

                // 3段目の処理
                if (rotatedBoardState[i, j, 2] != 0 && rotatedBoardState[i, j, 1] == 0)
                {
                    rotatedBoardState[i, j, 1] = rotatedBoardState[i, j, 2];
                    rotatedBoardState[i, j, 2] = 0;
                    if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                    {
                        rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                        rotatedBoardState[i, j, 1] = 0;
                    }
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    boardState[i, j, k] = rotatedBoardState[i, j, k];
                }
            }
        }

        //XZ座標を整える

        hasRotate = true;
        hasJudge = false;
        preRoate = "XRotate";
    }

    IEnumerator DelayX()
    {
        Count = 90 / Speed;

        for (int i = 0; i < Count; i++)
        {
            yield return new WaitForSeconds(0.01f);
            for (int j = 0; j < spawnedCubes.Count; j++)
            {
                spawnedCubes[j].transform.RotateAround(new Vector3(0, 0, 0), Vector3.left, Speed);
            }
        }
        RotationCheck = false;
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].GetComponent<BoxCollider>().enabled = true;
        }
        for (int i = 0;i < spawnedCubes.Count; i++)
        {
            spawnedCubes[i].GetComponent<BoxCollider>().enabled = true;
        }
        waku.SetActive(true);
    }


    public void ZRotate()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].GetComponent<BoxCollider>().enabled = false;
        }

        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();
            cubecontroller.canCollision = false;
            spawnedCubes[i].GetComponent<BoxCollider>().enabled = false;
            waku.SetActive(false);
        }

        if (RotationCheck == false)
        {
            RotationCheck = true;
            rePosition = false;
            StartCoroutine("DelayZ");
        }

        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();
            cubecontroller.cubePhsicsState = "falling";
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    rotatedBoardState[k, j, -i + 2] = boardState[i,j,k];
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // 2段目の処理
                if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                {
                    rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                    rotatedBoardState[i, j, 1] = 0;
                }

                // 3段目の処理
                if (rotatedBoardState[i, j, 2] != 0 && rotatedBoardState[i, j, 1] == 0)
                {
                    rotatedBoardState[i, j, 1] = rotatedBoardState[i, j, 2];
                    rotatedBoardState[i, j, 2] = 0;
                    if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                    {
                        rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                        rotatedBoardState[i, j, 1] = 0;
                    }
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    boardState[i, j, k] = rotatedBoardState[i, j, k];
                }
            }
        }

        hasRotate = true;
        hasJudge = false;
        preRoate = "ZRotate";
    }

    IEnumerator DelayZ()
    {
        Count = 90 / Speed;

        for (int i = 0; i < Count; i++)
        {
            yield return new WaitForSeconds(0.01f);
            for (int j = 0; j < spawnedCubes.Count; j++)
            {
                spawnedCubes[j].transform.RotateAround(new Vector3(0, 0, 0), Vector3.back, Speed);
            }

        }
        RotationCheck = false;
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].GetComponent<BoxCollider>().enabled = true;
        }
        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            spawnedCubes[i].GetComponent<BoxCollider>().enabled = true;
        }
        waku.SetActive(true);
    }

    public void ReXRotate()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].GetComponent<BoxCollider>().enabled = false;
        }

        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();
            cubecontroller.canCollision = false;
            spawnedCubes[i].GetComponent<BoxCollider>().enabled = false;
            waku.SetActive(false);
        }

        if (RotationCheck == false)
        {
            RotationCheck = true;
            rePosition = false;
            StartCoroutine("DelayReX");
        }

        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();
            cubecontroller.cubePhsicsState = "falling";
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    rotatedBoardState[i, k, -j + 2] = boardState[i,j,k];
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // 2段目の処理
                if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                {
                    rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                    rotatedBoardState[i, j, 1] = 0;
                }

                // 3段目の処理
                if (rotatedBoardState[i, j, 2] != 0 && rotatedBoardState[i, j, 1] == 0)
                {
                    rotatedBoardState[i, j, 1] = rotatedBoardState[i, j, 2];
                    rotatedBoardState[i, j, 2] = 0;
                    if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                    {
                        rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                        rotatedBoardState[i, j, 1] = 0;
                    }
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    boardState[i, j, k] = rotatedBoardState[i, j, k];
                }
            }
        }

        hasRotate = true;
        hasJudge = false;
        preRoate = "ReXRotate";
    }

    IEnumerator DelayReX()
    {
        Count = 90 / Speed;

        for (int i = 0; i < Count; i++)
        {
            yield return new WaitForSeconds(0.01f);
            for (int j = 0; j < spawnedCubes.Count; j++)
            {
                spawnedCubes[j].transform.RotateAround(new Vector3(0, 0, 0), Vector3.right, Speed);
            }

        }
        RotationCheck = false;
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].GetComponent<BoxCollider>().enabled = true;
        }
        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            spawnedCubes[i].GetComponent<BoxCollider>().enabled = true;
        }
        waku.SetActive(true);
    }

    public void ReZRotate()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].GetComponent<BoxCollider>().enabled = false;
        }

        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();
            cubecontroller.canCollision = false;
            spawnedCubes[i].GetComponent<BoxCollider>().enabled = false;
            waku.SetActive(false);
        }

        if (RotationCheck == false)
        {
            RotationCheck = true;
            rePosition = false;
            StartCoroutine("DelayReZ");
        }

        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();
            cubecontroller.cubePhsicsState = "falling";
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    rotatedBoardState[-k + 2, j, i] = boardState[i,j,k];
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // 2段目の処理
                if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                {
                    rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                    rotatedBoardState[i, j, 1] = 0;
                }

                // 3段目の処理
                if (rotatedBoardState[i, j, 2] != 0 && rotatedBoardState[i, j, 1] == 0)
                {
                    rotatedBoardState[i, j, 1] = rotatedBoardState[i, j, 2];
                    rotatedBoardState[i, j, 2] = 0;
                    if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                    {
                        rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                        rotatedBoardState[i, j, 1] = 0;
                    }
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    boardState[i, j, k] = rotatedBoardState[i, j, k];
                }
            }
        }

        hasRotate = true;
        hasJudge = false;
        preRoate = "ReZRotate";
    }

    IEnumerator DelayReZ()
    {
        Count = 90 / Speed;

        for (int i = 0; i < Count; i++)
        {
            yield return new WaitForSeconds(0.01f);
            for (int j = 0; j < spawnedCubes.Count; j++)
            {
                spawnedCubes[j].transform.RotateAround(new Vector3(0, 0, 0), Vector3.forward, Speed);
            }

        }
        RotationCheck = false;
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].GetComponent<BoxCollider>().enabled = true;
        }
        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            spawnedCubes[i].GetComponent<BoxCollider>().enabled = true;
        }
        waku.SetActive(true);
    }

    public void NoRotate()
    {
        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            tempX = spawnedCubes[i].transform.position.x;
            tempY = spawnedCubes[i].transform.position.y;
            tempZ = spawnedCubes[i].transform.position.z;

            spawnedCubes[i].transform.position = new Vector3(tempX, tempY, tempZ);

            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();
            cubecontroller.cubePhsicsState = "falling";
        }

        gameState = "CanPush";
        hasPush = false;
        preRoate = "";
    }

    public bool CheckHasFalled()
    {
        int hasFalledcude = 0;
        for (int i = 0; i < spawnedCubes.Count; i++)
        {
            cubecontroller = spawnedCubes[i].GetComponent<CubeController>();

            if (cubecontroller.cubePhsicsState == "hasfalled")
            {
                hasFalledcude++;
            }
        }
        if (hasFalledcude == spawnedCubes.Count && hasJudge == false)
        {
            hasJudge = true;
            gameState = "Judge";
        }

        if (hasFalledcude == spawnedCubes.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CheckWinner()
    {
        // チェックする可能性のある方向（横、縦、斜め、3D対角線）を定義
        Vector3[] directions = { Vector3.right, Vector3.up, Vector3.forward, new Vector3(1, 1, 0), new Vector3(-1, 1, 0), new Vector3(1, 0, 1), new Vector3(-1, 0, 1), new Vector3(0, 1, 1), new Vector3(0, 1, -1), new Vector3(1, 1, 1), new Vector3(-1, 1, 1), new Vector3(-1, 1, -1), new Vector3(1, -1, -1) };

        foreach (Vector3 direction in directions)
        {
            // 各方向ごとに、ゲームボードをスキャンして勝者がいるかどうかを判定
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        // 現在のセルがプレイヤーの識別子と一致している場合、連続しているかどうかを確認
                        if (boardState[x, y, z] == 1)
                        {
                            if (CheckDirection(x, y, z, 1, direction))
                            {
                                resultBoardState = boardState;
                                winner = "1";
                                changescene.Load("ResultScene");
                            }
                        }
                        if (boardState[x, y, z] == 2)
                        {
                            if (CheckDirection(x, y, z, 2, direction))
                            {
                                resultBoardState = boardState;
                                winner = "2";
                                changescene.Load("ResultScene");
                            }
                        }
                    }
                }
            }
        }
        if (hasRotate)
        {
            gameState = "CanPush";
            hasPush = false;
            hasRotate = false;
        }
        else
        {
            gameState = "Rotate";
        }
    }

    // 特定の方向で3つの連続したセルが揃っているかどうかを確認
    bool CheckDirection(int startX, int startY, int startZ, int current, Vector3 direction)
    {
        for (int i = 0; i < 3; i++)
        {
            int x = startX + (int)direction.x * i;
            int y = startY + (int)direction.y * i;
            int z = startZ + (int)direction.z * i;

            // ゲームボードの範囲外に出た場合は false を返す
            if (x < 0 || x >= 3 || y < 0 || y >= 3 || z < 0 || z >= 3)
            {
                return false;
            }

            // 連続していない場合は false を返す
            if (boardState[x, y, z] != current)
            {
                return false;
            }
        }

        return true;
    }


}
