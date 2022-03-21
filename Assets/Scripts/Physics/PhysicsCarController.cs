using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCarController : MonoBehaviour
{
    Rigidbody rb;
    bool canMove;

    [SerializeField]
    float forceMagnitude;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float rotSpeed;

    [SerializeField]
    float shotDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove)
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

        if(Input.GetKey("space"))
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, shotDistance))
            {
                Debug.Log("RAYCAST HIT!!!");
                if(hit.collider.CompareTag("car"))
                {
                    Debug.Log("RAYCAST HIT A CAR!!!");
                    Vector3 position = hit.rigidbody.position;

                    hit.rigidbody.AddExplosionForce(500, position, 15, 10);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            canMove = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            canMove = false;
        }
    }
}
