using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpell : MonoBehaviour
{
    public List<Func<UniTask>> spells;
    Animator animator;

    [SerializeField]
    int restTimeMs = 1000;
    private void Awake()
    {

        animator = GetComponentInChildren<Animator>();

        spells = new() {

        async () =>
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 1);

            animator.SetInteger("Index", 0);
            await UniTask.Delay(1000);
            animator.SetInteger("Index", 1);
            await UniTask.Delay(1000);
            animator.SetInteger("Index", 2);
            await UniTask.Delay(500);

            animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 0);
            animator.SetFloat("Blend", 0);
            await UniTask.Delay(restTimeMs);

        },

            async () =>

            {
            animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 1);

            animator.SetInteger("Index", 3);
            await UniTask.Delay(1000);
            animator.SetInteger("Index", 4);
            await UniTask.Delay(500);

            animator.SetLayerWeight(animator.GetLayerIndex("Spell"), 0);
            animator.SetFloat("Blend", 0);
            await UniTask.Delay(restTimeMs);
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
            await UniTask.Delay(restTimeMs);
        },

    };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
