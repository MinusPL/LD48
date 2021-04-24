using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public float minY = -3.0f;
    public float maxY = 3.0f;
    public float minX = -7.5f;
    public float maxX = 7.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = player.position - transform.position;

        float x = 0.0f;
        float y = 0.0f;

        if (diff.x >= minX && diff.x <= maxX)
        {
            x = diff.x * Time.deltaTime;
        }
        else
        {
            if (diff.x > maxX)
                x += diff.x - maxX;

            if (diff.x < minX)
                x += diff.x - minX;
        }

        if (diff.y >= minY && diff.y <= maxY)
        {
            y = diff.y * Time.deltaTime;
        }
        else
        {
            if (diff.y > maxY)
                y += diff.y - maxY;

            if (diff.y < minY)
                y += diff.y - minY;
        }


        transform.position += new Vector3(x, y, 0.0f);

        //Debug.Log(diff);
    }
}
