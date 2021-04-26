using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dolphin : MonoBehaviour
{
    public float movmentSpeed;

    private float dir = 1f;

    public void Start()
    {
        dir = Random.Range(0, 100) % 2 == 0 ? 1 : -1;
        transform.rotation = quaternion.Euler(0, dir == 1 ? 180 : 0, 0);
    }

    public void Update()
    {
        transform.position += Vector3.right * (dir * movmentSpeed * Time.deltaTime);
    }
}
