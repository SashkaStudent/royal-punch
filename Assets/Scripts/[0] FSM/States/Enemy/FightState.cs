using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FightState : BaseState
{
    EnemyFight fight;
    public override void DoWork(StateMachineAgent agent)
    {
    }

    public override void EnterState(StateMachineAgent agent)
    {
        
        fight = agent.GetComponent<EnemyFight>();
        fight.enabled = true;
        fight.SetAnimRound();
        stateName = "Fight";
        Test(agent);
        
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

        agent.TransitionToState("Spell");

    }
}
