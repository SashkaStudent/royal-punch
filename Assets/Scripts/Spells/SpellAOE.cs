using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public override async UniTask AttackFirst(int damage)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        transform.localScale = Vector3.zero;
        ps.Play();

        Physics.OverlapSphere(transform.position, 1.75f / 2)
            .ToList()
            .ForEach(c => {
                Accept(c.gameObject, damage);
             });
        await transform.DOScale(1.75f, ps.main.startLifetimeMultiplier).AsyncWaitForCompletion();
    }

    public override async UniTask AttackSecond(int damage)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        transform.localScale = Vector3.zero;
        ps.Play();

        Physics.OverlapSphere(transform.position, 1.75f)
            .ToList()
            .ForEach(c => {
                Accept(c.gameObject, damage);
            });

        
        await transform.DOScale(3.5f, ps.main.startLifetimeMultiplier).AsyncWaitForCompletion();
    }

    protected void Accept(GameObject hero, int damage)
    {
        if (hero.TryGetComponent(out HeroHealth health))
        {
            health.Punch(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.75f/2);
        Gizmos.DrawWireSphere(transform.position, 1.75f);
    }
}
