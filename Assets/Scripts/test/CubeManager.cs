using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private string cubeColor = "RedCube";

    void Start()
    {
        
    }

    public void GenerateCube((float, float) position)
    {
        GameObject obj = (GameObject)Resources.Load(cubeColor);
        // Cubeプレハブを元に、インスタンスを生成.
        GameObject newCube = Instantiate(obj);
        newCube.transform.parent = transform;
        newCube.transform.position = new Vector3(position.Item1, 1, position.Item2);
        SwitchCubeColor();
    }

    private void SwitchCubeColor()
    {
        if (cubeColor == "RedCube")
        {
            this.cubeColor = "BlueCube";
        }
        else {
            this.cubeColor = "RedCube";
        }
    }

    public bool CheckHasFalled()
    {
        return true;
    }
}
