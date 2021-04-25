using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 3.0f;
    private short dir = 1;
    void Start()
    {
        int rDir = Random.Range(0, 100);
        if (rDir % 2 == 0) { transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f); dir = -1; };
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * Vector3.right * moveSpeed * Time.deltaTime;
    }
}
