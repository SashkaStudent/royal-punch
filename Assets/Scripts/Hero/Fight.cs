using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Fight : MonoBehaviour
{
    [SerializeField]
    private float minDistance = 1f;
    public float MinDistance => minDistance;
    [SerializeField]
    private int damage = 10;

    private Animator animator;

    public event Action<int> OnHit;
    // Update is called once per frame
    [SerializeField]
    private int cooldownMs = 500;
    private bool cooldown = true;
    [Inject]
    EnemyHealth enemyHealth;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //   Punch();
    }
    void Update()
    {
        if (transform.position.magnitude <= minDistance && cooldown && enemyHealth.Current > 0)
        {

                animator.SetLayerWeight(1, 1);

                OnHit?.Invoke(damage);
                cooldown = false;
                UniTask.Delay(cooldownMs).ContinueWith(() => cooldown = true);
            
        }
        else
            animator.SetLayerWeight(1, 0);

    }

    //async UniTask Punch()
    //{
    //    while (true)
    //    {
    //        if(transform.position.magnitude <= minDistance)
    //        {
    //            OnHit?.Invoke();
    //            await UniTask.Delay(500);
    //        }
    //        await UniTask.DelayFrame(1);
    //    }
    //}
}
