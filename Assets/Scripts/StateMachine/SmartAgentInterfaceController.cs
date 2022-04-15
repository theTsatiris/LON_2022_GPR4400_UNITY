using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartAgentInterfaceController : MonoBehaviour
{
    public NPCStatable currentState;

    public ChaseState Chase = new ChaseState();
    public PatrolState Patrol = new PatrolState();
    public ReturnState Return = new ReturnState();

    [SerializeField]
    public Transform pointA;
    [SerializeField]
    public Transform pointB;

    [SerializeField]
    public Transform target;

    [SerializeField]
    float detectionDist;
    [SerializeField]
    float fieldOfView; //in degrees

    float detectionCosine;

    [HideInInspector]
    public NavMeshAgent nav;

    [HideInInspector]
    public bool onPatrol;

    [HideInInspector]
    public Vector3 lastPatrolPosition;

    // Start is called before the first frame update
    void Start()
    {
        onPatrol = true;

        nav = GetComponent<NavMeshAgent>();

        float halfFoV = fieldOfView / 2.0f;
        detectionCosine = Mathf.Cos(Mathf.Deg2Rad * halfFoV);

        lastPatrolPosition = transform.position;

        currentState = Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.DoState(this);
    }

    public bool CheckEngagement()
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