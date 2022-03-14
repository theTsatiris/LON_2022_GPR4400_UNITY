using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCarController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float forceMagnitude;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 totalForce = new Vector3();
        if (Input.GetKey("w"))
        {
            //rb.AddForce(transform.forward * forceMagnitude);
            totalForce += transform.forward * forceMagnitude;
        }
        if (Input.GetKey("a"))
        {
            //rb.AddForce(-transform.right * forceMagnitude);
            totalForce -= transform.right * forceMagnitude;
        }
        if (Input.GetKey("d"))
        {
            //rb.AddForce(transform.right * forceMagnitude);
            totalForce += transform.right * forceMagnitude;
        }

        Vector3 targetDir = Vector3.Normalize(totalForce);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, rotSpeed, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        if (Input.GetKey("s"))
        {
            //rb.AddForce(-transform.forward * forceMagnitude);
            totalForce -= transform.forward * forceMagnitude;
        }

        rb.AddForce(totalForce, ForceMode.Force);

        if(Vector3.Magnitude(rb.velocity) > maxSpeed)
        {
            Vector3 velocityNormalised = Vector3.Normalize(rb.velocity);
            rb.velocity = velocityNormalised * maxSpeed;
        }

        //Debug.Log(Vector3.Magnitude(rb.velocity));
    }
}
