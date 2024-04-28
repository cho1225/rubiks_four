// ResultSceneを制御
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    // 参照するスクリプト
    private Result result;
    // キューブをまとめて回すための親オブジェクト
    [SerializeField]private GameObject resultParentObject;

    //------------結果の表示に必要な設定

    [SerializeField] private Image mainImage;
    [SerializeField] private Sprite drawSpr;
    [SerializeField] private Sprite redWinSpr;
    [SerializeField] private Sprite blueWinSpr;

    [SerializeField] private GameObject ResultGrayCube;
    [SerializeField] private GameObject ResultRedCube;
    [SerializeField] private GameObject ResultBlueCube;

    //------------

    // 結果を表示
    void Start()
    {
        // Resultを探してとってくる
        GameObject resultObject = GameObject.Find("Result");
        if (resultObject != null)
        {
            result = resultObject.GetComponent<Result>();
            if (result == null)
            {
                Debug.Log("Resultコンポーネントが見つかりません");
                return;
            }
        }
        else
        {
            Debug.Log("Resultオブジェクトが見つかりません");
            return;
        }

        // 結果をセット
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (result.ResultBoardState[i, j, k] == CubeManager.CubeColor.Red)
                    {
                        GameObject newCube = Instantiate(ResultRedCube, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, j - 1, k - 1);
                    }
                    else if (result.ResultBoardState[i, j, k] == CubeManager.CubeColor.Blue)
                    {
                        GameObject newCube = Instantiate(ResultBlueCube, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, j - 1, k - 1);
                    }
                    else if (result.ResultBoardState[i, j, k] == CubeManager.CubeColor.Gray)
                    {
                        GameObject newCube = Instantiate(ResultGrayCube, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, j - 1, k - 1);
                    }
                }
            }
        }

        if (result.Winner == JudgeManager.Winner.Red)
        {
            mainImage.sprite = redWinSpr;
        }
        else if (result.Winner == JudgeManager.Winner.Blue)
        {
            mainImage.sprite = blueWinSpr;
        }
        else if (result.Winner == JudgeManager.Winner.Draw)
        {
            mainImage.sprite = drawSpr;
        }
        else
        {
            Debug.Log("winnerが取得できていません");
        }
    }

    private void Update()
    {
        // いい感じに見える回転の設定
        transform.Rotate(new Vector3(-7, -3, 5) * Time.deltaTime);
    }
}
