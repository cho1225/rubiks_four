using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    string result = GameController.winner;
    int[,,] state = GameController.resultBoardState;
    public GameObject resultParentObject;

    public GameObject mainImage;
    public Sprite redWinSpr;
    public Sprite blueWinSpr;

    // Start is called before the first frame update
    void Start()
    {
        GameObject redObj = (GameObject)Resources.Load("ResultRedCube");
        GameObject blueObj = (GameObject)Resources.Load("ResultBlueCube");
        GameObject grayObj = (GameObject)Resources.Load("ResultGrayCube");


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for(int k = 0; k < 3; k++)
                {
                    if (state[i, j, k] == 1)
                    {
                        GameObject newCube = Instantiate(redObj, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, k - 1, j - 1);
                    }
                    else if (state[i, j, k] == 2)
                    {
                        GameObject newCube = Instantiate(blueObj, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, k - 1, j - 1);
                    }
                    else if (state[i, j, k] == 3)
                    {
                        GameObject newCube = Instantiate(grayObj, resultParentObject.transform);
                        newCube.transform.position = new Vector3(i - 1, k - 1, j - 1);
                    }
                }
            }
        }

        if (result == "1")
        {
            mainImage.GetComponent<Image>().sprite = redWinSpr;
        }
        else if (result == "2")
        {
            mainImage.GetComponent<Image>().sprite = blueWinSpr;
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
