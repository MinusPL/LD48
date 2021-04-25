using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 2.0f;
    public float attackRange = 7.0f;
    public float attackSpeedMultiplier = 2.5f;

    public Animator animator;
    public Transform raycastSource;

    public short dir = 1;

    //"AI"
    private int state = 1;
    private bool playerInSight = false;

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
                animator.SetBool("SubInSight", false);
                speed = moveSpeed;
                if (playerInSight) state = 2;
                break;
            case 2:
                animator.SetBool("SubInSight", true);
                speed = moveSpeed * attackSpeedMultiplier;
                if (!playerInSight) state = 1;
                break;
		}

        transform.position += dir * Vector3.right * speed * Time.deltaTime;
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
