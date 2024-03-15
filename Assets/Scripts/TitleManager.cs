using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    // タイトルシーンのキューブを回転
    void Update()
    {
        transform.Rotate(new Vector3(-7, -3, 5) * Time.deltaTime);
    }
}
