using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroAgent : StateMachineAgent
{
    public Dictionary<Rigidbody, Quaternion> recorded = new();
   

    protected override void Start()
    {
        Animator animator = GetComponentInChildren<Animator>();

        //    agent.GetComponentInChildren<Animator>().speed = 0f;
        animator.enabled = false;

        animator.runtimeAnimatorController.animationClips.ToList().ForEach(c =>
        {
            if (c.name == "Armature|Idle2")
                c.SampleAnimation(animator.gameObject, 0);

            GetComponentsInChildren<Rigidbody>().ToList().ForEach(rb => {
                Record(rb);
            });

        });

        base.Start();
        States.Add("Movement", new MovementState());
        States.Add("Lie", new LieState());

        TransitionToState(entryState);
    }


    private void Record(Rigidbody rb)
    {
        recorded[rb] = rb.transform.localRotation;
    }

    private void Read(Rigidbody rb)
    {
        //rb.transform.rotation = recorded[rb];
     //   rb.transform.DORotateQuaternion(recorded[rb], 0.4f);
    }
}
