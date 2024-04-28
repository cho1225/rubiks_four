﻿// インゲームのUIを制御
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 回転用のボタンをまとめているオブジェクト
    [SerializeField] private GameObject rotateButton;
    // 枠のゲームオブジェクト
    [SerializeField] private GameObject waku;
    // 回転用のボタンの配列
    [SerializeField] private Button[] rotateButtonArray;
    // どちらの手番かを表すtext
    [SerializeField] private TextMeshProUGUI textMeshPro;

    // どちらの手番かを表すtextを設定
    public void SetText(CubeManager.CubeColor nextCubeColor)
    {
        if (nextCubeColor == CubeManager.CubeColor.Red)
        {
            textMeshPro.text = "red";
            textMeshPro.color = Color.red;
        }
        else if (nextCubeColor == CubeManager.CubeColor.Blue)
        {
            textMeshPro.text = "blue";
            textMeshPro.color = Color.blue;
        }
    }

    // ボタンがアクティブかを設定
    public void SetBottunActive(bool _bool)
    {
        rotateButton.SetActive(_bool);
    }

    // 枠がアクティブかを設定
    public void SetWakuActive(bool _bool)
    {
        waku.SetActive(_bool);
    }

    // 指定されたボタンを押せないようにする
    public void SetInteractiveButton(CubeRotater.Direction preRotate)
    {
        switch (preRotate)
        {
            case CubeRotater.Direction.X:
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
            case CubeRotater.Direction.Z:
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
            case CubeRotater.Direction.REX:
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
            case CubeRotater.Direction.REZ:
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
