using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSentryController : MonoBehaviour
{
    [SerializeField]
    Transform pointA;
    [SerializeField]
    Transform pointB;

    [SerializeField]
    float speed;

    [SerializeField]
    Transform target;
    [SerializeField]
    float detectionDist;
    [SerializeField]
    float chasingSpeed;

    [SerializeField]
    float maxReturnSpeed;

    float delta;
    int counter;

    bool onPatrol;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        onPatrol = true;
        //transform.position = pointA.position;
        counter = 0;
        delta = Mathf.Cos(counter * speed) * 0.5f + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= detectionDist) //CHASING MODE
        {
            onPatrol = false;
            transform.position = Vector3.MoveTowards(transform.position, target.position, chasingSpeed * Time.deltaTime);
        }
        else //PATROL MODE
        {
            if (onPatrol) //IDLE BEHAVIOUR
            {
                /*Vector3 force = pointB.position - pointA.position;
                Vector3 forceMirrored = -force;
                rb.AddForce((1 -  delta) * force + delta * forceMirrored);
                delta = Mathf.Cos(counter * speed) * 0.5f + 0.5f;
                counter++;*/

                transform.position = pointA.position * (1 - delta) + pointB.position * delta;
                delta = Mathf.Cos(counter * speed) * 0.5f + 0.5f;
                counter++;
            }
            else // RETURNING TO PATROL BEHAVIOUR
            {
                Vector3 posToReturnTo = pointA.position * (1 - delta) + pointB.position * delta;

                //transform.position = Vector3.MoveTowards(transform.position, posToReturnTo, chasingSpeed * Time.deltaTime);

                Vector3 force = posToReturnTo - transform.position;

                rb.AddForce(force);
                if(Vector3.Magnitude(rb.velocity) > maxReturnSpeed)
                {
                    rb.velocity = Vector3.Normalize(rb.velocity) * maxReturnSpeed;
                }

                float dist = Vector3.Distance(transform.position, posToReturnTo);
                if (dist <= 0.5f)
                {
                    onPatrol = true;
                }
            }
        }
    }
}