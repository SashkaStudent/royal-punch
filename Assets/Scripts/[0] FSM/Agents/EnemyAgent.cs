using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class EnemyAgent : StateMachineAgent
{
    [Inject]
    HeroHealth heroHealth;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        States.Add("Fight", new FightState());
        States.Add("Spell", new SpellState());
        States.Add("Lie", new LieState());
        States.Add("Victory", new VictoryState());

        heroHealth.OnDead += HeroDeadHandler; ;

        TransitionToState(entryState);
    }

    private void HeroDeadHandler()
    {
        IsWinner = true;
    }
}
