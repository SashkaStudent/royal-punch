using DG.Tweening;
using ModestTree.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyHealth : Health
{

    Animator animator;
    [Inject]
    Fight playerFight;
    [Inject]
    ComboCounter combo;

    [SerializeField]
    ParticleSystem hitParticles;


    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
        playerFight.OnHit += HitHandler;
    }
    //void Start()
    //{
    //    animator = GetComponentInChildren<Animator>();
    //    playerFight.OnHit += HitHandler;

    //    health = maxHealth;

    //}


    protected override void HitHandler(int value)
    {
        hitParticles.Play();
        DecreaseHealth((int)(value * combo.Combo));
        DOVirtual.Float(0f, 1f, 0.2f, v => animator.SetLayerWeight(animator.GetLayerIndex("GetHit"), v)).OnComplete(() => {
            DOVirtual.Float(1f, 0f, 0.2f, v => animator.SetLayerWeight(animator.GetLayerIndex("GetHit"), v)).OnComplete(() => {
                //    playerFight.OnHit += HitHandler;
                //     hitParticles.Stop();

            });
        });
    }


    protected override void OnDestroy()
    {
        playerFight.OnHit -= HitHandler;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
