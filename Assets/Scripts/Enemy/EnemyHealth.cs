using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    [Inject]
    Fight playerFight;
    [SerializeField]
    ParticleSystem hitParticles;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerFight.OnHit += HitHandler;
    }

    private void HitHandler()
    {
        playerFight.OnHit -= HitHandler;

        DOVirtual.Float(0f, 1f, 0.2f, v => animator.SetLayerWeight(animator.GetLayerIndex("GetHit"), v)).OnComplete(() => {
            hitParticles.Play();
            DOVirtual.Float(1f, 0f, 0.2f, v => animator.SetLayerWeight(animator.GetLayerIndex("GetHit"), v)).OnComplete(() => { 
            playerFight.OnHit += HitHandler;
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

    private void OnDestroy()
    {
        playerFight.OnHit -= HitHandler;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
