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

    public float maxSpawnHeight = -300;
    public float maxRectSize = 100;

    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var direction = player.transform.position - transform.position;
        if (direction.x < -maxRectSize * 0.8 && leftSpawner == null)
        {
            leftSpawner = Instantiate(gameObject, transform.position + Vector3.left * maxRectSize / 2, Quaternion.identity);
            leftSpawner.GetComponent<ParticleSpawner>().rightSpawner = gameObject;
            leftSpawner.GetComponent<ParticleSpawner>().maxSpawnHeight = maxSpawnHeight;
            leftSpawner.GetComponent<ParticleSpawner>().maxRectSize = maxRectSize;
        }
        if (direction.x > maxRectSize * 0.8 && rightSpawner == null)
        {
            rightSpawner = Instantiate(gameObject, transform.position + Vector3.right * maxRectSize / 2, Quaternion.identity);
            rightSpawner.GetComponent<ParticleSpawner>().leftSpawner = gameObject;
            rightSpawner.GetComponent<ParticleSpawner>().maxSpawnHeight = maxSpawnHeight;
            rightSpawner.GetComponent<ParticleSpawner>().maxRectSize = maxRectSize;
        }
        if (direction.y < -maxRectSize * 0.8 && downSpawner == null && transform.position.y + maxRectSize / 2 < maxSpawnHeight)
        {
            downSpawner = Instantiate(gameObject, transform.position + Vector3.down * maxRectSize / 2, Quaternion.identity);
            downSpawner.GetComponent<ParticleSpawner>().upSpawner = gameObject;
            downSpawner.GetComponent<ParticleSpawner>().maxSpawnHeight = maxSpawnHeight;
            downSpawner.GetComponent<ParticleSpawner>().maxRectSize = maxRectSize;
        }
        if (direction.y > maxRectSize * 0.8 && upSpawner == null)
        {
            upSpawner = Instantiate(gameObject, transform.position + Vector3.up * maxRectSize / 2, Quaternion.identity);
            upSpawner.GetComponent<ParticleSpawner>().downSpawner = gameObject;
            upSpawner.GetComponent<ParticleSpawner>().maxSpawnHeight = maxSpawnHeight;
            upSpawner.GetComponent<ParticleSpawner>().maxRectSize = maxRectSize;
        }
    }
}
