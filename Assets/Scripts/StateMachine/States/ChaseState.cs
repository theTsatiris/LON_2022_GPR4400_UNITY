using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : NPCStatable
{
    private SmartAgentInterfaceController smartAgent;

    public NPCStatable DoState(SmartAgentInterfaceController smartAgent)
    {
        this.smartAgent = smartAgent;

        DoChase();

        if(!this.smartAgent.CheckEngagement())
        {
            return this.smartAgent.Return;
        }
        else
            return this.smartAgent.currentState;
    }

    void DoChase()
    {
        if (smartAgent.onPatrol)
        {
            smartAgent.lastPatrolPosition = smartAgent.transform.position;
        }
        smartAgent.onPatrol = false;
        smartAgent.nav.SetDestination(smartAgent.target.position);
    }
}
