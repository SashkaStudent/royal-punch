using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class HeroAgent : StateMachineAgent
{
    public Dictionary<Rigidbody, Quaternion> recorded = new();

    [Inject]
    EnemyHealth enemyHealth;

    protected override void Start()
    {
        Record();

        base.Start();
        States.Add("Movement", new MovementState());
        States.Add("Lie", new LieState());
        States.Add("Victory", new VictoryState());

        enemyHealth.OnDead += EnemyDeadHandler;

        TransitionToState(entryState);
    }

    private void EnemyDeadHandler()
    {
        IsWinner = true;
    }

    private void Record()
    {
        Animator animator = GetComponentInChildren<Animator>();

        animator.enabled = false;

        animator.runtimeAnimatorController.animationClips.ToList().ForEach(c =>
        {
            if (c.name == "Armature|Idle2")
                c.SampleAnimation(animator.gameObject, 0);

            GetComponentsInChildren<Rigidbody>().ToList().ForEach(rb => {
                recorded[rb] = rb.transform.localRotation;
            });

        });
    }

}
