using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuboid : MonoBehaviour
{
    public float maxSpeed = 200.0f;
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = (-Vector3.right * maxSpeed);
        rigidbody.AddForce(velocity);
    }
}
