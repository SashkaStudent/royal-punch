using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
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

        Test(agent);
        
    }

    private void HealthChangedHandler(float newValue)
    {
        if (newValue <= 0)
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
      //  var tsc = new CancellationTokenSource();
      //  UniTask.Create(async () => { await UniTask.Delay(7000); Debug.Log("CANCEL!"); tsc.Cancel();});

          //  fight.SetAnimRound();
        await UniTask.Delay(5000);
        //  await UniTask.Create(fight.spells[0]).AttachExternalCancellation(tsc.Token);
        if(health.Current > 0)
          agent.TransitionToState("Spell");

    }
}
