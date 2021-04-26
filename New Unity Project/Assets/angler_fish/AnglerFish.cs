using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFish : MonoBehaviour
{
    public LevelManager levelManager;
    public float runawaySpeed = 8.0f;
    private bool runaway = false;
    private bool rotate = false;

    private float rotateTimer = 0.0f;
    public float rotateTime = 0.2f;
    private float targetRotation = 0.0f;
    private Quaternion last;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (runaway)
        {
            if (rotate)
            {
                rotateTimer += Time.deltaTime;
                float ratio = Mathf.Clamp(rotateTimer / rotateTime, 0.0f, 1.0f);
                float angle = Mathf.Lerp(last.eulerAngles.y, targetRotation, ratio);
                transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

                if (ratio == 1.0f)
                {
                    rotate = false;
                    rotateTimer = 0;
                    if (targetRotation == 360.0f)
                        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
            }
            else
            {
                transform.position += -transform.right * runawaySpeed * Time.deltaTime;
            }
        }
        if (Vector3.Distance(transform.position, Camera.main.transform.position) > 100.0f) Destroy(gameObject);
    }

	public void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
            levelManager.helenaCounter++;
            Vector3 direction = collision.gameObject.transform.right - -transform.right;  
            if(Vector3.Dot(-transform.right, direction) < 0)
			{
                float currentRot = transform.rotation.eulerAngles.y;
                targetRotation = currentRot == 0.0f ? 180.0f : 360.0f;
                rotate = true;
			}
            last = transform.rotation;
        }
        runaway = true;
	}
}
