using DG.Tweening;
using ModestTree.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int health = 100;
    public int Current => health;
    Animator animator;
    [Inject]
    Fight playerFight;
    [SerializeField]
    ParticleSystem hitParticles;

    public event Action<float> OnHealthChanged;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerFight.OnHit += HitHandler;
    }

    private void HitHandler(int damage)
    {
        //  playerFight.OnHit -= HitHandler;
        hitParticles.Play();
        DecreaseHealth(damage);
        DOVirtual.Float(0f, 1f, 0.2f, v => animator.SetLayerWeight(animator.GetLayerIndex("GetHit"), v)).OnComplete(() => {
            DOVirtual.Float(1f, 0f, 0.2f, v => animator.SetLayerWeight(animator.GetLayerIndex("GetHit"), v)).OnComplete(() => { 
        //    playerFight.OnHit += HitHandler;
           //     hitParticles.Stop();

            });
        });

        //Sequence sb = DOTween.Sequence();

        //sb.Append(DOVirtual.Float(0f, 1f, 0.5f, v => animator.SetLayerWeight(animator.GetLayerIndex("GetHit"), v))).OnComplete(() =>
        //{

        //});
        //sb.OnComplete(() => DOVirtual.Float(1f, 0f, 0.5f, v => animator.SetLayerWeight(animator.GetLayerIndex("GetHit"), v)));

        //playerFight.OnHit += HitHandler;


    }

    public void DecreaseHealth(int value)
    {
        health = (int)MathF.Max(0, health - value);
        OnHealthChanged?.Invoke(health);
    }

    private void OnDestroy()
    {
        if(playerFight != null)
        playerFight.OnHit -= HitHandler;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
