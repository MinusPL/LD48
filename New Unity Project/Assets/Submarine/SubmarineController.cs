using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    public Rigidbody rigidbody;

    //Steering
    public float maxSpeed = 5.0f;
    public float acceleration = 2.0f;
    public float braking = 3.5f;
    private float currentSpeed = 0.0f;
    public float maxAngle = 15.0f;

    private short currentDirection = 1;
    private bool rotating = false;
    public float rotateSpeed = 360.0f;
    public float rotateTime = 1.0f;
    private float rotateTimer = 0.0f;
    private Quaternion last;

    //Health and stuff

    public float maxHealth = 800.0f;
    private float currentHealth;

    //Amount of damage multiplied by magnitude of hit
    public float damageFromHit = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, v, 0.0f).normalized;

        if(!rotating)
		{
            if (direction.x < 0 && currentDirection != -1)
            {
                rotating = true;
                currentDirection = -1;
                last = transform.rotation;
            }
            if (direction.x > 0 && currentDirection != 1)
            {
                rotating = true;
                currentDirection = 1;
                last = transform.rotation;
            }
        }

        if (!rotating)
        {

            if (direction.magnitude > 0.05f)
            {
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = currentSpeed > maxSpeed ? maxSpeed : currentSpeed;
            }
            else
            {
                currentSpeed = 0.0f;
            }

            Vector3 velocity = direction * currentSpeed;
            rigidbody.AddForce(velocity);
            transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, (maxAngle * direction.y));
        }
        else
		{
            rotateTimer += Time.deltaTime;
            float ratio = Mathf.Clamp(rotateTimer / rotateTime, 0.0f, 1.0f);
            float targetRotation;
            if (currentDirection == -1)
                targetRotation = 180.0f;
            else
                targetRotation = 360.0f;
            float angle = Mathf.Lerp(last.eulerAngles.y, targetRotation, ratio);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            if (ratio == 1.0f)
            {
                rotating = false;
                rotateTimer = 0;
                if (targetRotation == 360.0f)
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
        }

        //TODO
        if(currentHealth <= 0.0f)
		{
            Destroy(gameObject);
		}
    }

	private void OnCollisionEnter(Collision collision)
	{
        if(collision.relativeVelocity.magnitude > 0.1f)
            currentHealth -= collision.relativeVelocity.magnitude * damageFromHit;

	}
}
