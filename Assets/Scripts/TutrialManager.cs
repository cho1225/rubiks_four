// 操作方法を制御
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutrialManager : MonoBehaviour
{
    // 表示中の画像
    [SerializeField] private Image tutrialImage;
    // 表示する画像の配列
    [SerializeField] private Sprite[] tutrialSprites;
    // 表示中の画像のページ番号を表すtext
    [SerializeField] private TextMeshProUGUI pageIndexText;
    // 次のページに進めるボタン
    [SerializeField] private Button nextButton;
    // 前のページに戻るボタン
    [SerializeField] private Button backButton;
    // 表示中の画像のページ番号
    private int pageIndex;
    // チュートリアルのページ数
    private int pageIndexMax;
    // ページ番号が変わったかどうか
    private bool isPageIndexChange = false;

    // チュートリアルの設定を初期化
    public void InitializeTutrial()
    {
        pageIndexMax = tutrialSprites.Length;
        pageIndex = 0;
        tutrialImage.sprite = tutrialSprites[0];
        pageIndexText.text = (pageIndex + 1).ToString();
        nextButton.interactable = true;
        backButton.interactable = false;

    }

    // 表示中の画像のページ番号を表すtextにページ番号を設定
    public void UpdateImage()
    {
        // ページ番号が変わったときのみ実行
        if (isPageIndexChange)
        {
            // ボタンがインタラクティブかを設定
            if (pageIndex >= pageIndexMax -1)
            {
                nextButton.interactable = false;
            }
            else if (pageIndex <= 0)
            {
                backButton.interactable = false;
            }
            else
            {
                nextButton.interactable = true;
                backButton.interactable = true;
            }
            // 表示中の画像のページ番号を表すtextにページ番号を設定
            pageIndexText.text = (pageIndex + 1).ToString();
            // 表示中の画像を設定
            tutrialImage.sprite = tutrialSprites[pageIndex];
            isPageIndexChange = false;
        }
    }

    // ページ番号を次に進める
    public void NextPage()
    {
        if (pageIndex < pageIndexMax)
        {
            pageIndex++;
            isPageIndexChange = true;
        }
    }

    // ページ番号を前に戻す
    public void BackPage()
    {
        if (pageIndex > 0)
        {
            pageIndex--;
            isPageIndexChange = true;
        }
    }
}
