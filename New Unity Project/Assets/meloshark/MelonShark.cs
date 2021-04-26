using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MelonShark : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 1.0f;
    public float attackRange = 7.0f;
    public float attackSpeedMultiplier = 2.5f;

    public Transform raycastSource;

    public short dir = 1;
    public float minDistanceFromPlayer = 4.0f;
    public float randomMovingTime = 10.0f;
    private float rmTimer = 0.0f;
    public float rotationSpeed = 100;

    //"AI"
    private bool locked = false;
    private int state = 1;
    private bool playerInSight = false;
    private Transform playerTransform;
    private float angle;
    private float radius;
    private Vector3 target;
    private Vector3 source;
    public float estimatedMoveTime = 0.0f;
    private float moveTime = 0.0f;

    void Start()
    {
        InvokeRepeating("SearchForPlayer", 0.5f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.0f;
        switch(state)
        {
            case 1:
                speed = moveSpeed;
                if (playerInSight && !locked)
                {
                    state = 2;
                    moveTime = 0.0f;
                }
                transform.position += Vector3.right * (dir * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, dir == 1 ? 0 : 180, 0);
                break;
            case 2:
                rmTimer += Time.deltaTime;
                generateTargetPos();
                state = 3;
                break;
            case 3:
                if ((target - transform.position).normalized.x < 0 && transform.rotation.eulerAngles.y != 180)
                {
                    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                    if ((transform.rotation.eulerAngles.y > 175) && (transform.rotation.eulerAngles.y < 185))
                    {
                        transform.rotation = Quaternion.Euler(0, 180 , 0);
                        state = 4;
                    }
                }
                else if ((target - transform.position).normalized.x > 0 && transform.rotation.eulerAngles.y != 0)
                {
                    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                    if (transform.rotation.eulerAngles.y < 5 || transform.rotation.eulerAngles.y > 355)
                    {
                        transform.rotation = Quaternion.Euler(0, 0 , 0);
                        state = 4;
                    }
                }
                else
                {
                    state = 4;
                }
                break;
            case 4:
                rmTimer += Time.deltaTime;
                moveTowardsTarget();
                if (Vector3.Distance(transform.position, target) < 1) state = 2;
                else if (rmTimer >= randomMovingTime) { state = 5; locked = true; rmTimer = 0.0f; moveTime = 0.0f; }
                break;
            case 5:
                if (transform.rotation.eulerAngles.y != 0 && dir == 1)
                {
                    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                    if (transform.rotation.eulerAngles.y < 5 || transform.rotation.eulerAngles.y > 355)
                    {
                        transform.rotation = Quaternion.Euler(0, 0 , 0);
                        state = 1;
                    }
                }
                else if (transform.rotation.eulerAngles.y != 180 && dir == -1)
                {
                    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                    if ((transform.rotation.eulerAngles.y > 175) && (transform.rotation.eulerAngles.y < 185))
                    {
                        transform.rotation = Quaternion.Euler(0, 180 , 0);
                        state = 1;
                    }
                }
                else
                {
                    state = 1;
                }
                break;
        }

        if (Vector3.Distance(transform.position, Camera.main.transform.position) > 100.0f) Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(target, transform.position);
    }

    public void SearchForPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                RaycastHit hit;
                if (Physics.Raycast(raycastSource.position, (collider.transform.position - raycastSource.position).normalized,
                    out hit, attackRange))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        playerInSight = true;
                        playerTransform = hit.transform;
                        break;
                    }
                    else
                    {
                        playerInSight = false;
                    }
                }
                return;
            }
        }

    }

    private void generateTargetPos()
	{
        source = transform.position;
        while (true)
        {
            float x = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x);
            float y = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);

            Vector3 pos = new Vector3(x, y, 0.0f);
            if (HandleUtility.DistancePointLine(playerTransform.position, transform.position, pos) <
                minDistanceFromPlayer)
            {
                continue;
            }
            if (Vector3.Distance(pos, playerTransform.position) > minDistanceFromPlayer)
			{
                target = pos;
                break;
			}
        }
        float fullLen = Vector3.Distance(source, target);
        estimatedMoveTime = fullLen / moveSpeed;
    }

    private void moveTowardsTarget()
    {
        Vector3 diff = target - source;
        transform.position += diff.normalized * moveSpeed * Time.deltaTime;
        /*moveTime += Time.deltaTime;
        if (moveTime < estimatedMoveTime)
        {
            float ratio = moveTime / estimatedMoveTime;
            Vector3 diff = target - source;
            transform.position = source + diff * ratio;

            //{10.0, 5.0}
        }
        else
		{
            transform.position = target;
		}*/
    }
}
