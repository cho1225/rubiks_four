using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject rotateButton;
    public GameObject waku;
    public Button[] rotateButtonArray = new Button[4];

    public void SetBottunActive(bool _bool)
    {
        rotateButton.SetActive(_bool);
    }

    public void SetWakuActive(bool _bool)
    {
        waku.SetActive(_bool);
    }

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
