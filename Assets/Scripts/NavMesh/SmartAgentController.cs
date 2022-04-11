using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartAgentController : MonoBehaviour
{
    enum NPCState
    {
        Patrol,
        Chase,
        Return
    }

    private NPCState currentState;

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

    Vector3 lastPatrolPosition;

    // Start is called before the first frame update
    void Start()
    {
        onPatrol = true;

        nav = GetComponent<NavMeshAgent>();

        float halfFoV = fieldOfView / 2.0f;
        detectionCosine = Mathf.Cos(Mathf.Deg2Rad * halfFoV);

        lastPatrolPosition = transform.position;

        currentState = NPCState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case NPCState.Patrol:
                DoPatrol();
                break;
            case NPCState.Chase:
                DoChase();
                break;
            case NPCState.Return:
                DoReturn();
                break;
            default:
                break;
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

    void DoPatrol()
    {
        if (Vector3.Distance(transform.position, pointA.position) <= 1.0f)
        {
            Debug.Log("DEST: POINT B");
            nav.SetDestination(pointB.position);
        }
        if (Vector3.Distance(transform.position, pointB.position) <= 1.0f)
        {
            Debug.Log("DEST: POINT A");
            nav.SetDestination(pointA.position);
        }
    }

    void DoChase()
    {
        if (onPatrol)
        {
            lastPatrolPosition = transform.position;
        }
        onPatrol = false;
        nav.SetDestination(target.position);
    }

    void DoReturn()
    {
        nav.SetDestination(lastPatrolPosition);
        if (Vector3.Distance(transform.position, lastPatrolPosition) <= 1.0f)
        {
            onPatrol = true;
            float distanceA = Vector3.Distance(lastPatrolPosition, pointA.position);
            float distanceB = Vector3.Distance(lastPatrolPosition, pointB.position);
            if (distanceA <= distanceB)
                nav.SetDestination(pointA.position);
            else
                nav.SetDestination(pointB.position);
        }
    }
}
