using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSphereController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float torqueMagnitude;

    [SerializeField]
    float jumpForceMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("GRAVITY: " + Physics.gravity);
        if(Input.GetKey("w"))
        {
            rb.AddTorque(new Vector3(torqueMagnitude, 0.0f, 0.0f));
        }
        if (Input.GetKey("a"))
        {
            rb.AddTorque(new Vector3(0.0f, 0.0f, torqueMagnitude));
        }
        if (Input.GetKey("s"))
        {
            rb.AddTorque(new Vector3(-torqueMagnitude, 0.0f, 0.0f));
        }
        if (Input.GetKey("d"))
        {
            rb.AddTorque(new Vector3(0.0f, 0.0f, -torqueMagnitude));
        }

        if (Input.GetKey("space"))
        {
            if(transform.position.y <= 0.77f)
            { 
                rb.AddForce(Vector3.up * jumpForceMagnitude, ForceMode.Impulse);
            }
        }
    }
}
