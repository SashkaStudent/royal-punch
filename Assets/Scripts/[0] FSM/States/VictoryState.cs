using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryState : BaseState
{
    public override void DoWork(StateMachineAgent agent)
    {
        
    }

    public override void EnterState(StateMachineAgent agent)
    {

        agent.GetComponent<Victory>().Apply();

    }

    public override void ExitState(StateMachineAgent agent)
    {
        
    }
}
