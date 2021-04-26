using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dolphin : MonoBehaviour
{
    public float movmentSpeed = 1;

    public float dir = 1f;

    public void Start()
    {
        transform.rotation = quaternion.Euler(0, dir == 1 ? 0 : 180, 0);
    }

    public void Update()
    {
        transform.position += Vector3.right * (dir * movmentSpeed * Time.deltaTime);
    }
}
