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
    Transform target;
    [SerializeField]
    float detectionDist;

    [SerializeField]
    float deltaSpeed;

    [SerializeField]
    float patrolSpeed;
    [SerializeField]
    float chasingSpeed;
    [SerializeField]
    float maxReturnSpeed;

    [SerializeField]
    float fieldOfView; //in degrees
    
    float detectionCosine;

    float delta;
    int counter;

    bool onPatrol;

    Rigidbody rb;

    Transform patrolPointToLookAt;

    private Vector3 forceVector;
    private Vector3 forceVectorMirrored;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        onPatrol = true;
        //transform.position = pointA.position;
        counter = 0;
        delta = Mathf.Cos(counter * deltaSpeed) * 0.5f + 0.5f;

        patrolPointToLookAt = pointA;

        float halfFoV = fieldOfView / 2.0f;

        detectionCosine = Mathf.Cos(Mathf.Deg2Rad * halfFoV);

        forceVector = Vector3.Normalize(pointB.position - pointA.position) * patrolSpeed;
        forceVectorMirrored = new Vector3(-2.0f * forceVector.x, forceVector.y, -2.0f * forceVector.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (CheckEngagement()) //CHASING MODE
        {
            onPatrol = false;
            transform.LookAt(target.position);

            Vector3 forceDir = Vector3.Normalize(target.position - transform.position);

            float finalSpeed = (distance / detectionDist) * chasingSpeed;

            rb.AddForce(forceDir * finalSpeed);

            //Simple chasing without forces.....
            //transform.position = Vector3.MoveTowards(transform.position, target.position, chasingSpeed);
        }
        else //PATROL MODE
        {
            if (onPatrol) //IDLE/PATROL BEHAVIOUR
            {
                delta = Mathf.Cos(counter * deltaSpeed) * 0.5f + 0.5f;

                //THE BAD WAY!!!!!
                /*Vector3 force = pointB.position - pointA.position;
                Vector3 forceMirrored = -force;
                rb.AddForce((1 -  delta) * force + delta * forceMirrored);
                delta = Mathf.Cos(counter * deltaSpeed) * 0.5f + 0.5f;
                counter++;*/

                //THE RIGHT WAY!!!
                /*float newX = (1 - delta) * forceVector.x + delta * forceVectorMirrored.x;
                float newZ = (1 - delta) * forceVector.z + delta * forceVectorMirrored.z;

                Vector3 patrolForce = new Vector3(newX, 0.0f, newZ);

                Debug.Log(patrolForce.x);
                Debug.Log(patrolForce.z);
                Debug.Log(Vector3.Magnitude(patrolForce));
                Debug.Log("-------------------------");

                rb.AddForce(patrolForce);*/

                //Simple patrol with LERPing between positions...
                transform.position = pointA.position * (1 - delta) + pointB.position * delta;

                //Debug.Log(delta);
                if (delta < 0.001f) 
                {
                    patrolPointToLookAt = pointB;
                }
                if(delta > 0.999f)
                {
                    patrolPointToLookAt = pointA;
                }

                transform.LookAt(patrolPointToLookAt);
                counter++;
            }
            else // RETURNING TO PATROL BEHAVIOUR
            {
                Vector3 posToReturnTo = pointA.position * (1 - delta) + pointB.position * delta;

                transform.LookAt(posToReturnTo);
                //transform.position = Vector3.MoveTowards(transform.position, posToReturnTo, chasingSpeed * Time.deltaTime);

                Vector3 force = posToReturnTo - transform.position;

                rb.AddForce(force);

                float dist = Vector3.Distance(transform.position, posToReturnTo);

                if (dist > 3.0f)
                {
                    rb.velocity = Vector3.Normalize(rb.velocity) * maxReturnSpeed;
                }

                if (dist <= 0.5f)
                {
                    onPatrol = true;
                }
            }
        }
    }

    bool CheckEngagement()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        Vector3 vecToTarg = Vector3.Normalize(target.position - transform.position);

        float dotProd = Vector3.Dot(transform.forward, vecToTarg);

        if((dotProd >= detectionCosine) && (distance <= detectionDist))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}