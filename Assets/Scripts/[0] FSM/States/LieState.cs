using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class LieState : BaseState
{
    Dictionary<Rigidbody, Quaternion> recorded = new();
    Health health;
    public override void DoWork(StateMachineAgent agent)
    {
    }

    public override void EnterState(StateMachineAgent agent)
    {
        stateName = "Lie";

        Animator animator = agent.GetComponentInChildren<Animator>();
        

        health = agent.GetComponent<Health>();
        var ct = new CancellationTokenSource();

        void activateCt() { ct.Cancel(); health.OnDead -= activateCt; }

        if (health.Current <= 0) activateCt();
        else
            health.OnDead += activateCt;

        //    agent.GetComponentInChildren<Animator>().speed = 0f;
        animator.enabled = false;

        //animator.runtimeAnimatorController.animationClips.ToList().ForEach(c =>
        //{
        //    Debug.Log(c.name);

        //});


        Vector3 hip = Vector3.zero;


        agent.GetComponentsInChildren<Rigidbody>().ToList().ForEach(rb =>
        {
            if (rb.CompareTag("Hip")) { 
                
                hip = rb.position; }
            rb.isKinematic = false;
            rb.velocity = -agent.transform.forward;
        });
        if (agent is HeroAgent ha) recorded = ha.recorded;



        if(agent is HeroAgent)
        UniTask.Delay(2000, cancellationToken: ct.Token).ContinueWith(

           async () =>
           {

               agent.GetComponentsInChildren<Rigidbody>()
               .ToList()
               .ForEach(rb => { if (rb.CompareTag("Hip")) rb.AddForce((Vector3.up + agent.transform.forward) * 1000); });
               //.Where(rb => rb.CompareTag("Hip"))
               //.FirstOrDefault()
               //.AddForce(100*(Vector3.up));

               agent.GetComponentsInChildren<Rigidbody>().ToList().ForEach(rb =>
                    {
                        Read(rb);
                        rb.isKinematic = true;
                        if (rb.CompareTag("Hip")) rb.transform.DOMove(hip, 0.4f);

                        //rb.velocity = -agent.transform.forward;
                    });
           }
                    
            //agent.TransitionToState("Movement")
            ).ContinueWith(async () => {  
                await UniTask.Delay(500); 
                animator.enabled = true; 
                await UniTask.Delay(200); 
                agent.TransitionToState("Movement"); 
            });
    }

    public override void ExitState(StateMachineAgent agent)
    {
    }

    private void Record(Rigidbody rb)
    {
        recorded[rb] = rb.transform.rotation;
    }

    private void Read(Rigidbody rb)
    {
        //rb.transform.rotation = recorded[rb];
        rb.transform.DOLocalRotateQuaternion(recorded[rb], 0.4f);
    }
}

