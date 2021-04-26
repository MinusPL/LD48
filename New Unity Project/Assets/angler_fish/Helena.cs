using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helena : MonoBehaviour
{
    public float maxChaseDistance;
    public float speed;
    private GameObject target;
    public Rigidbody body;
    public float rotationSpeed;


    private int state = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
		{
            switch(state)
			{
                case 1:
                    if (target != null) state = 2;
                    break;
                case 2:
                    if((Vector3.Distance(transform.position, target.transform.position) > maxChaseDistance))
					{
                        target = null;
                        state = 1;
					}
					else
					{
                        Vector3 dir = (target.transform.position - transform.position).normalized;
                        body.AddForce(dir * speed);
                        state = 3;
                    }
                    break;
                case 3:
                    if ((target.transform.position - transform.position).normalized.x > 0 && transform.rotation.eulerAngles.y != 180)
                    {
                        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                        if ((transform.rotation.eulerAngles.y > 175) && (transform.rotation.eulerAngles.y < 185))
                        {
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            state = 2;
                        }
                    }
                    else if ((target.transform.position - transform.position).normalized.x < 0 && transform.rotation.eulerAngles.y != 0)
                    {
                        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                        if (transform.rotation.eulerAngles.y < 5 || transform.rotation.eulerAngles.y > 355)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            state = 2;
                        }
                    }
                    else
                    {
                        state = 2;
                    }
                    break;
			}
        }
        if (Vector3.Distance(transform.position, Camera.main.transform.position) > 100.0f) Destroy(gameObject);
    }

	public void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Player"))
		{
            target = other.gameObject;
		}
	}
}
