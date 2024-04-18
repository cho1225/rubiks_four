using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    // �Q�Ƃ���X�N���v�g
    private Result result;
    // �L���[�u���܂Ƃ߂ĉ񂷂��߂̐e�I�u�W�F�N�g
    [SerializeField]private GameObject resultParentObject;

    //------------���ʂ̕\���ɕK�v�Ȑݒ�

    [SerializeField]private Image mainImage;
    [SerializeField] private Sprite redWinSpr;
    [SerializeField] private Sprite blueWinSpr;

    [SerializeField] private GameObject ResultGrayCube;
    [SerializeField] private GameObject ResultRedCube;
    [SerializeField] private GameObject ResultBlueCube;

    //------------

    // ���ʂ�\��
    void Start()
    {
        // Result��T���ĂƂ��Ă���
        GameObject resultObject = GameObject.Find("Result");
        if (resultObject != null)
        {
            result = resultObject.GetComponent<Result>();
            if (result == null)
            {
                Debug.Log("Result�R���|�[�l���g��������܂���");
                return;
            }
        }
        else
        {
            Debug.Log("Result�I�u�W�F�N�g��������܂���");
            return;
        }

        // ���ʂ��Z�b�g
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (result.ResultBoardState[i, j, k] == 1)
                    {
                        GameObject newCube = Instantiate(ResultRedCube, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, j - 1, k - 1);
                    }
                    else if (result.ResultBoardState[i, j, k] == 2)
                    {
                        GameObject newCube = Instantiate(ResultBlueCube, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, j - 1, k - 1);
                    }
                    else if (result.ResultBoardState[i, j, k] == 0)
                    {
                        GameObject newCube = Instantiate(ResultGrayCube, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, j - 1, k - 1);
                    }
                }
            }
        }

        if (result.Winner == 1)
        {
            mainImage.sprite = redWinSpr;
        }
        else if (result.Winner == 2)
        {
            mainImage.sprite = blueWinSpr;
        }
        else
        {
            Debug.Log("winner���擾�ł��Ă��܂���");
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(-7, -3, 5) * Time.deltaTime);
    }
}
