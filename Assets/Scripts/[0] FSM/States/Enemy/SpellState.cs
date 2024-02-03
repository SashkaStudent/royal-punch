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
        UniTask.Create(enemySpell.spells[2])
            .ContinueWith(()=>agent.TransitionToState("Fight"));
    }

    public override void ExitState(StateMachineAgent agent)
    {
    }

    
}
