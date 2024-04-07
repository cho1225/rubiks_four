using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ��]�p�̃{�^�����܂Ƃ߂Ă���I�u�W�F�N�g
    [SerializeField] private GameObject rotateButton;
    // �g�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject waku;
    // ��]�p�̃{�^���̔z��
    [SerializeField] private Button[] rotateButtonArray;
    // �ǂ���̎�Ԃ���\��text
    [SerializeField] private TextMeshProUGUI textMeshPro;

    // �ǂ���̎�Ԃ���\��text��ݒ�
    public void SetText(int nextCubeColor)
    {
        if (nextCubeColor == 1)
        {
            textMeshPro.text = "red";
            textMeshPro.color = Color.red;
        }
        else if (nextCubeColor == 2)
        {
            textMeshPro.text = "blue";
            textMeshPro.color = Color.blue;
        }
    }

    // �{�^�����A�N�e�B�u����ݒ�
    public void SetBottunActive(bool _bool)
    {
        rotateButton.SetActive(_bool);
    }

    // �g���A�N�e�B�u����ݒ�
    public void SetWakuActive(bool _bool)
    {
        waku.SetActive(_bool);
    }

    // �w�肳�ꂽ�{�^���������Ȃ��悤�ɂ���
    public void SetInteractiveButton(string preRotate)
    {
        switch (preRotate)
        {
            case "x":
            for (int i = 0; i < rotateButtonArray.Length; i++)
            {
                if (i == 2)
                {
                    rotateButtonArray[i].interactable = false;
                }
                else
                {
                     rotateButtonArray[i].interactable = true;
                }
            }
            break;
            case "z":
                for (int i = 0; i < rotateButtonArray.Length; i++)
                {
                    if (i == 3)
                    {
                        rotateButtonArray[i].interactable = false;
                    }
                    else
                    {
                        rotateButtonArray[i].interactable = true;
                    }
                }
                break;
            case "rex":
                for (int i = 0; i < rotateButtonArray.Length; i++)
                {
                    if (i == 0)
                    {
                        rotateButtonArray[i].interactable = false;
                    }
                    else
                    {
                        rotateButtonArray[i].interactable = true;
                    }
                }
                break;
            case "rez":
                for (int i = 0; i < rotateButtonArray.Length; i++)
                {
                    if (i == 1)
                    {
                        rotateButtonArray[i].interactable = false;
                    }
                    else
                    {
                        rotateButtonArray[i].interactable = true;
                    }
                }
                break;
            default:
                for (int i = 0; i < rotateButtonArray.Length; i++)
                {
                    rotateButtonArray[i].interactable = true;
                }
                break;
        }

    }
}
