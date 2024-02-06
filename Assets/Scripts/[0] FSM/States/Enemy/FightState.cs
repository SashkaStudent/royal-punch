using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Zenject;

public class FightState : BaseState
{
    EnemyFight fight;
    EnemyHealth health;

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
        this.agent = agent;

        fight = agent.GetComponent<EnemyFight>();
        health = agent.GetComponent<EnemyHealth>();

        fight.enabled = true;
        fight.SetAnimRound();
        stateName = "Fight";

        health.OnHealthChanged += HealthChangedHandler;

        agent.GetComponentInChildren<Animator>().enabled = true;
        //  agent.GetComponentInChildren<Animator>().speed = 1f;

        agent.GetComponentsInChildren<Rigidbody>().ToList().ForEach(rb => {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        });


        UniTask.Delay(Random.Range(2000, 5000)).ContinueWith(() => {
            if (health.Current > 0 && !agent.IsWinner)
                agent.TransitionToState("Spell");
        });

        
    }

    private void HealthChangedHandler(int health, int value)
    {
        if (health <= 0)
        {
            agent.TransitionToState("Lie");
        }
    }

    public override void ExitState(StateMachineAgent agent)
    {
        fight.enabled = false;
    }

    async UniTask Test(StateMachineAgent agent)
    {

        await UniTask.Delay(5000);
        if (health.Current > 0 && !agent.IsWinner)
            agent.TransitionToState("Spell");
        else if (agent.IsWinner)
            agent.TransitionToState("Victory");

    }
}
