using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCubeController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(-7, -3, 5) * Time.deltaTime);
    }
}
