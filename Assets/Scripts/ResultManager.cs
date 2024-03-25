using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    private Result result;
    [SerializeField]private GameObject resultParentObject;

    [SerializeField]private Image mainImage;
    [SerializeField] private Sprite redWinSpr;
    [SerializeField] private Sprite blueWinSpr;

    [SerializeField] private GameObject ResultGrayCube;
    [SerializeField] private GameObject ResultRedCube;
    [SerializeField] private GameObject ResultBlueCube;

    // Start is called before the first frame update
    void Start()
    {
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

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (result.GetResultBoardState()[i, j, k] == 1)
                    {
                        GameObject newCube = Instantiate(ResultRedCube, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, j - 1, k - 1);
                    }
                    else if (result.GetResultBoardState()[i, j, k] == 2)
                    {
                        GameObject newCube = Instantiate(ResultBlueCube, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, j - 1, k - 1);
                    }
                    else if (result.GetResultBoardState()[i, j, k] == 0)
                    {
                        GameObject newCube = Instantiate(ResultGrayCube, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, j - 1, k - 1);
                    }
                }
            }
        }

        if (result.GetWinner() == 1)
        {
            mainImage.sprite = redWinSpr;
        }
        else if (result.GetWinner() == 2)
        {
            mainImage.sprite = blueWinSpr;
        }
        else
        {
            Debug.Log("winnerが取得できていません");
        }
    }
}
