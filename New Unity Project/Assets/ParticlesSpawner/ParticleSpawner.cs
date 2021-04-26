using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject upSpawner;

    public GameObject downSpawner;

    public GameObject leftSpawner;

    public GameObject rightSpawner;

    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var direction = player.transform.position - transform.position;

        if (direction.x < -80 && leftSpawner == null)
        {
            leftSpawner = Instantiate(gameObject, transform.position + Vector3.left * 100, Quaternion.identity);
            leftSpawner.GetComponent<ParticleSpawner>().rightSpawner = gameObject;
        }
        if (direction.x > 80 && rightSpawner == null)
        {
            rightSpawner = Instantiate(gameObject, transform.position + Vector3.right * 100, Quaternion.identity);
            rightSpawner.GetComponent<ParticleSpawner>().leftSpawner = gameObject;
        }
        if (direction.y < -80 && downSpawner == null)
        {
            downSpawner = Instantiate(gameObject, transform.position + Vector3.down * 100, Quaternion.identity);
            downSpawner.GetComponent<ParticleSpawner>().upSpawner = gameObject;
        }
        if (direction.y > 80 && upSpawner == null)
        {
            upSpawner = Instantiate(gameObject, transform.position + Vector3.up * 100, Quaternion.identity);
            upSpawner.GetComponent<ParticleSpawner>().downSpawner = gameObject;
        }
    }
}
