using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float speed = 1.0f;
    public string cubeColor;
    public string cubePhsicsState = "falling";

    public GameController gamecontroller;

    public void CubeStart()
    {
        GameObject gameObject = GameObject.Find("GameObject");
        gamecontroller = gameObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    public void CubeUpdate()
    {
        speed = 1.0f;
        if (gamecontroller.rePosition)
        {
            MoveCube();
        }

        if (gamecontroller.RotationCheck == false)
        {
            RePositionXZ();

            if (cubePhsicsState == "hasfalled")
            {
                RePositionY();
            }
        }
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
            transform.Translate(0, -speed * Time.deltaTime, 0, Space.World);
        }
    }

    /// <summary>
    /// kokokarasakiyabai
    /// </summary>

    public void RePositionXZ()
    {
        //xç¿ïWÇêÆÇ¶ÇÈ
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
        //zç¿ïWÇêÆÇ¶ÇÈ
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
    }

    public void RePositionY()
    {
        //yç¿ïWÇêÆÇ¶ÇÈ
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

    void OnCollisionEnter(Collision collision)
    {
        if (gamecontroller.rePosition)
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
