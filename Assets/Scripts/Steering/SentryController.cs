using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryController : MonoBehaviour
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

    float delta;
    int counter;

    bool onPatrol;

    // Start is called before the first frame update
    void Start()
    {
        onPatrol = true;
        counter = 0;
        delta = Mathf.Cos(counter * speed) * 0.5f + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance <= detectionDist) //CHASING MODE
        {
            onPatrol = false;
            transform.position = Vector3.MoveTowards(transform.position, target.position, chasingSpeed * Time.deltaTime);
        }
        else //PATROL MODE
        {
            if (onPatrol) //IDLE BEHAVIOUR
            {
                transform.position = pointA.position * (1 - delta) + pointB.position * delta;
                delta = Mathf.Cos(counter * speed) * 0.5f + 0.5f;
                counter++;
            }
            else // RETURNING TO PATROL BEHAVIOUR
            {
                Vector3 posToReturnTo = pointA.position * (1 - delta) + pointB.position * delta;
                transform.position = Vector3.MoveTowards(transform.position, posToReturnTo, chasingSpeed * Time.deltaTime);
                float dist = Vector3.Distance(transform.position, posToReturnTo);
                if(dist <= 0.5f)
                {
                    onPatrol = true;
                }
            }
        }
    }
}
