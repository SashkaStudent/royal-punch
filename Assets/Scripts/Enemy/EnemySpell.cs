using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpell : MonoBehaviour
{
    [Inject]
    EnemyHealth health;
    [Inject]
    EnemyFight fight;

    [SerializeField]
    private List<GameObject> spellPrefabs;
    public List<Func<UniTask>> spells;
    Animator animator;

    [SerializeField]
    int restTimeMs = 1000;
    [SerializeField]
    private List<int> restTimesMs;
    private void Awake()
    {

        animator = GetComponentInChildren<Animator>();

        spells = new() {

        async () =>
        {

            DOVirtual.Float(0f, 1f, 1f, v => animator.SetLayerWeight(animator.GetLayerIndex("Spell"), v));

            animator.SetInteger("Index", 0);
            Spell aoe = Instantiate(spellPrefabs[0], transform.position, Quaternion.identity).GetComponent<Spell>();



            await aoe.Preparation();
            animator.SetInteger("Index", 1);
            if(health.Current <= 0)
            {
                Destroy(aoe.gameObject);
                return;
            }
            await aoe.AttackFirst(fight.Damage * 2);
            animator.SetInteger("Index", 2);
            await UniTask.Delay(200);

            await aoe.AttackSecond(fight.Damage * 2);

            DOVirtual.Float(0.5f, 0f, 0.5f, v => animator.SetFloat("Blend", v));
            DOVirtual.Float(1f, 0f, 1f, v => animator.SetLayerWeight(animator.GetLayerIndex("Spell"), v));

            await UniTask.Delay(restTimesMs[0]);
            Destroy(aoe.gameObject);
        },

            async () =>

            {

            DOVirtual.Float(0f, 1f, 1f, v => animator.SetLayerWeight(animator.GetLayerIndex("Spell"), v));
            animator.SetInteger("Index", 3);

            Spell aoe = Instantiate(spellPrefabs[0], transform.position, Quaternion.identity).GetComponent<Spell>();
            await aoe.Preparation();
            if(health.Current <= 0)
            {
                Destroy(aoe.gameObject);
                return;
            }

            animator.SetInteger("Index", 4);
            await UniTask.Delay(200);

            await aoe.AttackSecond(fight.Damage * 2);

            DOVirtual.Float(0.5f, 0f, 0.5f, v => animator.SetFloat("Blend", v));

            DOVirtual.Float(1f, 0f, 1f, v => animator.SetLayerWeight(animator.GetLayerIndex("Spell"), v));
            await UniTask.Delay(restTimesMs[1]);
            Destroy(aoe.gameObject);
        },

        //    async () =>
        //{
        //    animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 1);

        //    animator.SetInteger("Index", 5);
        //    await UniTask.Delay(1000);
        //                if(health.Current <= 0)
        //    {
        //     //   Destroy(aoe.gameObject);
        //        return;
        //    }

        //    animator.SetInteger("Index", 6);
        //    await UniTask.Delay(600);

        //    animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 0);
        //    animator.SetFloat("Blend", 0);
        //    await UniTask.Delay(restTimesMs[2]);
        //},

    };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
