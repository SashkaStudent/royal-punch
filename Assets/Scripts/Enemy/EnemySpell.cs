using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpell : MonoBehaviour
{
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
            Sequence s = DOTween.Sequence();

            //animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 1);
            s.Append(DOVirtual.Float(0f, 1f, 1f, v => animator.SetLayerWeight(animator.GetLayerIndex("Spell"), v)));

            animator.SetInteger("Index", 0);
            Spell aoe = Instantiate(spellPrefabs[0], transform.position, Quaternion.identity).GetComponent<Spell>();
            await aoe.Preparation();
            animator.SetInteger("Index", 1);
            await aoe.AttackFirst();
            animator.SetInteger("Index", 2);
            await UniTask.Delay(200);

            await aoe.AttackSecond();



            //animator.SetFloat("Blend", 0);
            Sequence sb = DOTween.Sequence();
            sb.Append(DOVirtual.Float(0.5f, 0f, 0.5f, v => animator.SetFloat("Blend", v)));
            s = DOTween.Sequence();
            s.Append(DOVirtual.Float(1f, 0f, 1f, v => animator.SetLayerWeight(animator.GetLayerIndex("Spell"), v)));

            //animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 0);
            await UniTask.Delay(restTimesMs[0]);
            Destroy(aoe.gameObject);
        },

            async () =>

            {
            Sequence s = DOTween.Sequence();

            //animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 1);
            s.Append(DOVirtual.Float(0f, 1f, 1f, v => animator.SetLayerWeight(animator.GetLayerIndex("Spell"), v)));
            animator.SetInteger("Index", 3);

            Spell aoe = Instantiate(spellPrefabs[0], transform.position, Quaternion.identity).GetComponent<Spell>();
            await aoe.Preparation();
            
     
            animator.SetInteger("Index", 4);
            await UniTask.Delay(200);

            await aoe.AttackSecond();

            Sequence sb = DOTween.Sequence();
            sb.Append(DOVirtual.Float(0.5f, 0f, 0.5f, v => animator.SetFloat("Blend", v)));
            s = DOTween.Sequence();
            s.Append(DOVirtual.Float(1f, 0f, 1f, v => animator.SetLayerWeight(animator.GetLayerIndex("Spell"), v)));

            //animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 0);
            await UniTask.Delay(restTimesMs[1]);
            Destroy(aoe.gameObject);
        },

            async () =>
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 1);

            animator.SetInteger("Index", 5);
            await UniTask.Delay(1000);
            animator.SetInteger("Index", 6);
            await UniTask.Delay(600);

            animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 0);
            animator.SetFloat("Blend", 0);
            await UniTask.Delay(restTimesMs[2]);
        },

    };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
