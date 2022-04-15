using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : NPCStatable
{
    SmartAgentInterfaceController smartAgent;

    public NPCStatable DoState(SmartAgentInterfaceController smartAgent)
    {
        this.smartAgent = smartAgent;

        DoPatrol();

        if(this.smartAgent.CheckEngagement())
        {
            return this.smartAgent.Chase;
        }
        else
        {
            return this.smartAgent.currentState;
        }
    }

    void DoPatrol()
    {
        if (Vector3.Distance(smartAgent.transform.position, smartAgent.pointA.position) <= 1.0f)
        {
            Debug.Log("DEST: POINT B");
            smartAgent.nav.SetDestination(smartAgent.pointB.position);
        }
        if (Vector3.Distance(smartAgent.transform.position, smartAgent.pointB.position) <= 1.0f)
        {
            Debug.Log("DEST: POINT A");
            smartAgent.nav.SetDestination(smartAgent.pointA.position);
        }
    }
}
