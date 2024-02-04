using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField]
    private float minDistance = 1f;
    public float MinDistance => minDistance;

    private Animator animator;

    public event Action OnHit;
    // Update is called once per frame

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Punch();
    }
    void Update()
    {
        if(transform.position.magnitude <= minDistance)
        {
        
            animator.SetLayerWeight(1, 1);
            
        } else
        {
            animator.SetLayerWeight(1, 0);

        }
    }

    async UniTask Punch()
    {
        while (true)
        {
            if(transform.position.magnitude <= minDistance)
            {
                OnHit?.Invoke();
                await UniTask.Delay(500);
            }
            await UniTask.DelayFrame(1);
        }
    }
}
