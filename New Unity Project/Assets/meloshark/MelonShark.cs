using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonShark : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 2.0f;
    public float attackRange = 7.0f;
    public float attackSpeedMultiplier = 2.5f;
    
    public Transform raycastSource;

    public short dir = 1;

    //"AI"
    private int state = 1;
    private bool playerInSight = false;
    private Transform playerTransform;
    private float angle;
    private float radius;

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
                if (playerInSight)
                {
                    if (Vector3.Distance(playerTransform.position, transform.position) < attackRange)
                    {
                        state = 2;
                        angle = Mathf.Deg2Rad * Vector2.SignedAngle(playerTransform.position, transform.position);
                        radius = Vector3.Distance(transform.position, playerTransform.position);
                        Debug.Log(angle);
                    }
                }
                transform.position += Vector3.right * (dir * speed * Time.deltaTime);
                break;
            case 2:
                //speed = moveSpeed * attackSpeedMultiplier;
                speed = moveSpeed;
                angle += speed * Time.deltaTime;
                var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                transform.position = playerTransform.position + offset;
                Debug.Log(Mathf.Deg2Rad * Vector2.SignedAngle(playerTransform.position, transform.position));
                if (!playerInSight) state = 1;
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
}
