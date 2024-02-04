using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellState : BaseState
{
    private EnemySpell enemySpell;
    public override void DoWork(StateMachineAgent agent)
    {
    }

    public override void EnterState(StateMachineAgent agent)
    {
        stateName = "Spell";
        enemySpell = agent.GetComponent<EnemySpell>();
        int rand = Random.Range(0,enemySpell.spells.Count);
        UniTask.Create(enemySpell.spells[rand])
            .ContinueWith(()=>agent.TransitionToState("Fight"));
    }

    public override void ExitState(StateMachineAgent agent)
    {
    }

    
}
