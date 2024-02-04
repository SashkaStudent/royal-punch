using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAOE : Spell
{

    public override async UniTask Preparation()
    {
        transform.localScale = Vector3.zero;
        await transform.DOScale(3.5f, 2f).AsyncWaitForCompletion();
        Destroy(transform.GetChild(0).gameObject);


        //HeroHealth.Hit();
    }

    public override async UniTask AttackFirst()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        transform.localScale = Vector3.zero;
        ps.Play();
        await transform.DOScale(1.75f, ps.main.startLifetimeMultiplier).AsyncWaitForCompletion();
    }

    public override async UniTask AttackSecond()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        transform.localScale = Vector3.zero;
        ps.Play();
        await transform.DOScale(3.5f, ps.main.startLifetimeMultiplier).AsyncWaitForCompletion();
    }
}
