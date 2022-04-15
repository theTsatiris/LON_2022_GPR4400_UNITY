using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NPCStatable
{
    NPCStatable DoState(SmartAgentInterfaceController smartAgent);
}
