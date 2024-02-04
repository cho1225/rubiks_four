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
        // �����_���ȃC���f�b�N�X��I��
        int xRandomIndex = Random.Range(0, 3);
        int zRandomIndex = Random.Range(0, 3);

        GameObject obj = (GameObject)Resources.Load("GrayCube");
        // Cube�v���n�u�����ɁA�C���X�^���X�𐶐��A
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

        //����̔z�����]
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

        //��]��̔���̔z��̗���
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // 2�i�ڂ̏���
                if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                {
                    rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                    rotatedBoardState[i, j, 1] = 0;
                }

                // 3�i�ڂ̏���
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

        //XZ���W�𐮂���

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
                // 2�i�ڂ̏���
                if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                {
                    rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                    rotatedBoardState[i, j, 1] = 0;
                }

                // 3�i�ڂ̏���
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
                // 2�i�ڂ̏���
                if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                {
                    rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                    rotatedBoardState[i, j, 1] = 0;
                }

                // 3�i�ڂ̏���
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
                // 2�i�ڂ̏���
                if (rotatedBoardState[i, j, 1] != 0 && rotatedBoardState[i, j, 0] == 0)
                {
                    rotatedBoardState[i, j, 0] = rotatedBoardState[i, j, 1];
                    rotatedBoardState[i, j, 1] = 0;
                }

                // 3�i�ڂ̏���
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
        // �`�F�b�N����\���̂�������i���A�c�A�΂߁A3D�Ίp���j���`
        Vector3[] directions = { Vector3.right, Vector3.up, Vector3.forward, new Vector3(1, 1, 0), new Vector3(-1, 1, 0), new Vector3(1, 0, 1), new Vector3(-1, 0, 1), new Vector3(0, 1, 1), new Vector3(0, 1, -1), new Vector3(1, 1, 1), new Vector3(-1, 1, 1), new Vector3(-1, 1, -1), new Vector3(1, -1, -1) };

        foreach (Vector3 direction in directions)
        {
            // �e�������ƂɁA�Q�[���{�[�h���X�L�������ď��҂����邩�ǂ����𔻒�
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        // ���݂̃Z�����v���C���[�̎��ʎq�ƈ�v���Ă���ꍇ�A�A�����Ă��邩�ǂ������m�F
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

    // ����̕�����3�̘A�������Z���������Ă��邩�ǂ������m�F
    bool CheckDirection(int startX, int startY, int startZ, int current, Vector3 direction)
    {
        for (int i = 0; i < 3; i++)
        {
            int x = startX + (int)direction.x * i;
            int y = startY + (int)direction.y * i;
            int z = startZ + (int)direction.z * i;

            // �Q�[���{�[�h�͈̔͊O�ɏo���ꍇ�� false ��Ԃ�
            if (x < 0 || x >= 3 || y < 0 || y >= 3 || z < 0 || z >= 3)
            {
                return false;
            }

            // �A�����Ă��Ȃ��ꍇ�� false ��Ԃ�
            if (boardState[x, y, z] != current)
            {
                return false;
            }
        }

        return true;
    }


}
