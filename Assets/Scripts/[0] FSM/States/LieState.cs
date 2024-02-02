using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieState : BaseState
{
    public override void DoWork(StateMachineAgent agent)
    {
    }

    public override void EnterState(StateMachineAgent agent)
    {
        stateName = "Lie";
    }

    public override void ExitState(StateMachineAgent agent)
    {
    }

}
