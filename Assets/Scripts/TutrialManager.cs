using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutrialManager : MonoBehaviour
{
    // チュートリアル表示用のパネル
    [SerializeField] GameObject tutrialPanel;

    // チュートリアルを表示するかを制御する
    public void SetTutrial()
    {
        tutrialPanel.SetActive(!tutrialPanel.activeSelf);
    }
}
