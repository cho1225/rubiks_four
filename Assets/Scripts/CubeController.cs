using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeController : MonoBehaviour
{
    public float speed = 1.0f;
    public string cubeColor;
    public string cubePhsicsState = "falling";
    public bool canCollision = false;
    public GameController gamecontroller;

    public void CubeStart()
    {
        GameObject gameObject = GameObject.Find("GameObject");
        gamecontroller = gameObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    public void CubeUpdate()
    {
        Debug.Log(canCollision);
        speed = 1.0f;
        if (gamecontroller.rePosition)
        {
            MoveCube();
        }

        if (gamecontroller.RotationCheck == false && gamecontroller.rePosition == false)
        {
            RePositionXYZ();
        }

        if (gamecontroller.RotationCheck == false && cubePhsicsState == "hasfalled")
        {
            RePositionY();
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        canCollision = true;
    }

    public void MoveCube()
    {
        if (transform.position.y <= -(gamecontroller.boardSize - 1) / 2)
        {
            speed = 0.0f;
            cubePhsicsState = "hasfalled";
        }
        else
        {
            StartCoroutine(Delay());
            transform.Translate(0, -speed * Time.deltaTime, 0, Space.World);
        }
    }

    /// <summary>
    /// kokokarasakiyabai
    /// </summary>

    public void RePositionXYZ()
    {
        //x座標を整える
        if (transform.position.x < -0.5f)
        {
            transform.position = new Vector3(-1.0f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= -0.5f && transform.position.x < 0.5f)
        {
            transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(1.0f, transform.position.y, transform.position.z);
        }
        //y座標を整える
        if (transform.position.y < -0.5f)
        {
            transform.position = new Vector3(transform.position.x, -1.0f, transform.position.z);
        }
        else if (transform.position.y >= -0.5f && transform.position.y < 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
        }
        //z座標を整える
        if (transform.position.z < -0.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f);
        }
        else if (transform.position.z >= -0.5f && transform.position.z < 0.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1.0f);
        }
        gamecontroller.rePosition = true;
        cubePhsicsState = "falling";
    }

    public void RePositionY()
    {
        //y座標を整える
        if (transform.position.y < -0.5f)
        {
            transform.position = new Vector3(transform.position.x, -1.0f, transform.position.z);
        }
        else if (transform.position.y >= -0.5f && transform.position.y < 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (gamecontroller.RotationCheck == false && canCollision)
        {
            if (collision.gameObject.CompareTag("Cube"))
            {
                speed = 0.0f;
                cubePhsicsState = "hasfalled";
            }
            else
            {
                speed = 1.0f;
            }
        }
    }
}
