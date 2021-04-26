using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject upSpawner;

    public GameObject downSpawner;

    public GameObject leftSpawner;

    public GameObject rightSpawner;

    public float fromSpawnHeight = -100;
    public float toSpawnHeight = -300;
    public float maxRectSize = 100;
    public float margin = 0.5f;
    private void Start()
    {
    }

    void Update()
    {
        Debug.Log(transform.position.y + maxRectSize / 2);
        if (transform.position.y + maxRectSize / 2 > fromSpawnHeight || transform.position.y - maxRectSize / 2 < toSpawnHeight)
        {
            foreach (var ps in GetComponentsInChildren<ParticleSystem>())
            {
                ps.Stop();
                ps.Clear();
            }
        }
        var player = GameObject.FindGameObjectWithTag("Player");
        var direction = player.transform.position - transform.position;
        if (direction.x < -maxRectSize/2 * margin && leftSpawner == null)
        {
            leftSpawner = Instantiate(gameObject, transform.position + Vector3.left * maxRectSize, Quaternion.identity);
            leftSpawner.GetComponent<ParticleSpawner>().rightSpawner = gameObject;
            leftSpawner.GetComponent<ParticleSpawner>().toSpawnHeight = toSpawnHeight;
            leftSpawner.GetComponent<ParticleSpawner>().maxRectSize = maxRectSize;
        }
        if (direction.x > maxRectSize/2 * margin && rightSpawner == null)
        {
            rightSpawner = Instantiate(gameObject, transform.position + Vector3.right * maxRectSize, Quaternion.identity);
            rightSpawner.GetComponent<ParticleSpawner>().leftSpawner = gameObject;
            rightSpawner.GetComponent<ParticleSpawner>().toSpawnHeight = toSpawnHeight;
            rightSpawner.GetComponent<ParticleSpawner>().maxRectSize = maxRectSize;
        }
        if (direction.y < -maxRectSize/2 * margin && downSpawner == null)
        {
            downSpawner = Instantiate(gameObject, transform.position + Vector3.down * maxRectSize, Quaternion.identity);
            downSpawner.GetComponent<ParticleSpawner>().upSpawner = gameObject;
            downSpawner.GetComponent<ParticleSpawner>().toSpawnHeight = toSpawnHeight;
            downSpawner.GetComponent<ParticleSpawner>().maxRectSize = maxRectSize;
        }
        if (direction.y > maxRectSize/2 * margin && upSpawner == null)
        {
            upSpawner = Instantiate(gameObject, transform.position + Vector3.up * maxRectSize, Quaternion.identity);
            upSpawner.GetComponent<ParticleSpawner>().downSpawner = gameObject;
            upSpawner.GetComponent<ParticleSpawner>().toSpawnHeight = toSpawnHeight;
            upSpawner.GetComponent<ParticleSpawner>().maxRectSize = maxRectSize;
        }
    }
}
