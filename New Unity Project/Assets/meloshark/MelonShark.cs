using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                break;
            case 2:
                rmTimer += Time.deltaTime;
                generateTargetPos();
                state = 3;
                break;
            case 3:
                rmTimer += Time.deltaTime;
                moveTowardsTarget();
                if (transform.position == target) state = 2;
                else if (rmTimer >= randomMovingTime) { state = 1; locked = true; rmTimer = 0.0f; moveTime = 0.0f; }
                break;
        }

        /*if (playerTransform)
        {
            Debug.Log(Vector3.Distance(playerTransform.position, transform.position));
        }*/
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
        moveTime += Time.deltaTime;
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
		}
    }
}
