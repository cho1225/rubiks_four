// TitleSceneを制御
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
        // いい感じに見える回転の設定
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
