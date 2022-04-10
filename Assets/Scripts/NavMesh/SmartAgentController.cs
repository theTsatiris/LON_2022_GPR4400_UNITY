using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartAgentController : MonoBehaviour
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
    float fieldOfView; //in degrees

    float detectionCosine;

    NavMeshAgent nav;

    bool onPatrol;

    bool switched;

    Vector3 lastPatrolPosition;

    // Start is called before the first frame update
    void Start()
    {
        onPatrol = true;

        switched = false;

        nav = GetComponent<NavMeshAgent>();

        float halfFoV = fieldOfView / 2.0f;
        detectionCosine = Mathf.Cos(Mathf.Deg2Rad * halfFoV);
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckEngagement()) // CHASING MODE
        {
            onPatrol = false;
            lastPatrolPosition = transform.position;
            nav.SetDestination(target.position);
        }
        else
        {
            if(onPatrol) // PATROL/IDLE MODE
            {
                //TODO!!!!!!!!
                //if(Vector3.Distance(transform.position, pointA.position) <= 1.0f)
                //{
                //    Debug.Log("DEST: POINT B");
                //    nav.SetDestination(pointB.position);
                //}
                //else
                //{
                //    Debug.Log("DEST: POINT A");
                //    nav.SetDestination(pointA.position);
                //}
            }
            else // RETURN TO PATROL MODE
            {
                nav.SetDestination(lastPatrolPosition);
                if(Vector3.Distance(transform.position, lastPatrolPosition) <= 1.0f)
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

        if ((dotProd >= detectionCosine) && (distance <= detectionDist))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
