using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : NPCStatable
{
    SmartAgentInterfaceController smartAgent;

    public NPCStatable DoState(SmartAgentInterfaceController smartAgent)
    {
        this.smartAgent = smartAgent;

        DoReturn();

        if(this.smartAgent.CheckEngagement())
        {
            return this.smartAgent.Chase;
        }
        if(this.smartAgent.onPatrol)
        {
            return this.smartAgent.Patrol;
        }

        return this.smartAgent.currentState;
    }

    void DoReturn()
    {
        smartAgent.nav.SetDestination(smartAgent.lastPatrolPosition);
        if (Vector3.Distance(smartAgent.transform.position, smartAgent.lastPatrolPosition) <= 1.0f)
        {
            smartAgent.onPatrol = true;
            float distanceA = Vector3.Distance(smartAgent.lastPatrolPosition, smartAgent.pointA.position);
            float distanceB = Vector3.Distance(smartAgent.lastPatrolPosition, smartAgent.pointB.position);
            if (distanceA <= distanceB)
                smartAgent.nav.SetDestination(smartAgent.pointA.position);
            else
                smartAgent.nav.SetDestination(smartAgent.pointB.position);
        }
    }
}
