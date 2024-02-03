using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAgent : StateMachineAgent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        States.Add("Fight", new FightState());
        States.Add("Spell", new SpellState());

        TransitionToState(entryState);
    }

}
