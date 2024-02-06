using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementState : BaseState
{
    Movement movement;
    HeroHealth health;
    StateMachineAgent agent;
    public override void DoWork(StateMachineAgent agent)
    {
        if (agent.IsWinner)
        {
            agent.TransitionToState("Victory");
        }
    }

    public override void EnterState(StateMachineAgent agent)
    {
        stateName = "Move";

        agent.GetComponent<Movement>().enabled = true;
        agent.GetComponent<Fight>().enabled = true;
        health = agent.GetComponent<HeroHealth>();
        this.agent = agent;
        //if (agent.TryGetComponent(out movement))
        //    movement.enabled = true;
        
        agent.GetComponentInChildren<Animator>().enabled = true;
      //  agent.GetComponentInChildren<Animator>().speed = 1f;

        agent.GetComponentsInChildren<Rigidbody>().ToList().ForEach(rb => {
            rb.isKinematic = true; 
            rb.velocity = Vector3.zero;
        });

        health.OnPunch += PunchHandler;
        health.OnDead += PunchHandler;

        //   UniTask.Delay(5000).ContinueWith(()=>agent.TransitionToState("Lie"));

    }

    private void PunchHandler()
    {
        agent.TransitionToState("Lie");
    }

    public override void ExitState(StateMachineAgent agent)
    {
        if (agent.TryGetComponent(out movement))
            movement.enabled = false;
        agent.GetComponent<Fight>().enabled = false;

        health.OnPunch -= PunchHandler;

    }

}
