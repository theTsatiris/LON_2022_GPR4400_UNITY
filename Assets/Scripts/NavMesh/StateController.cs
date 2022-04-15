using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateController : MonoBehaviour
{
    SmartAgentController agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = FindObjectOfType<SmartAgentController>();      
    }

    // Update is called once per frame
    void Update()
    {
        foreach(NPCState candidateState in Enum.GetValues(typeof(NPCState)))
        {
            if (agent.UpdateState(candidateState))
                agent.currentState = candidateState;
        }
    }
}
