// インゲームのUIを制御
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
