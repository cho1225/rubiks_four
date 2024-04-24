using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // 参照するスクリプト
    [SerializeField] private TutrialManager tutrialManager;
    // チュートリアル表示用のパネル
    [SerializeField] private GameObject tutrialPanel;

    // タイトルシーンのキューブを回転
    void Update()
    {
        transform.Rotate(new Vector3(-7, -3, 5) * Time.deltaTime);

        if (tutrialPanel.activeSelf)
        {
            tutrialManager.UpdateImage();
        }
    }

    // チュートリアルを表示するかを制御する
    public void SetTutrial()
    {
        tutrialPanel.SetActive(!tutrialPanel.activeSelf);
        tutrialManager.InitializeTutrial();
    }
}
