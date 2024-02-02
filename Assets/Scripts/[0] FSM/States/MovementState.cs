using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : BaseState
{
    Movement movement;

    public override void DoWork(StateMachineAgent agent)
    {

    }

    public override void EnterState(StateMachineAgent agent)
    {
        stateName = "Move";

        if(agent.TryGetComponent(out movement))
            movement.enabled = true;
        
    }

    public override void ExitState(StateMachineAgent agent)
    {
        if (agent.TryGetComponent(out movement))
            movement.enabled = false;
    }

}
