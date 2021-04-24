using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    public Rigidbody rigidbody;

    public float maxSpeed = 5.0f;

    public float acceleration = 2.0f;

    public float braking = 3.5f;

    private float currentSpeed = 0.0f;

    public float maxAngle = 15.0f;

    private float currentAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, v, 0.0f).normalized;

        if (direction != Vector3.zero)
		{
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = currentSpeed > maxSpeed ? maxSpeed : currentSpeed;
		}
		else
		{
            currentSpeed -= braking * Time.deltaTime;
            currentSpeed = currentSpeed < 0.0f ? 0.0f : currentSpeed;
        }

        Vector3 velocity = direction * currentSpeed;

        rigidbody.AddForce(velocity);

        transform.eulerAngles = new Vector3(0.0f, 0.0f, (maxAngle * direction.y) + 90.0f);
    }
}
