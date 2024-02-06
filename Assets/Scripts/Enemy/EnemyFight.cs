using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFight : MonoBehaviour
{

    [SerializeField]
    private float angleSpeed = 45f;
    [SerializeField]
    private float dotThreashold = -0.7f;

    Animator animator;

    [Inject]
    Fight playerFight;

    float blend = 0.5f;

    [SerializeField]
    private int cooldownMs = 500;
    private bool cooldown = true;

    [SerializeField]
    private int damage = 20;
    public int Damage => damage;
    public event Action<int> OnHit;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(playerFight.transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, angleSpeed * Time.deltaTime);

        float dot = Vector3.Dot(playerFight.transform.forward, transform.forward);

        if (playerFight.isActiveAndEnabled && playerFight.transform.position.magnitude < playerFight.MinDistance && dot < dotThreashold)
        {
            Sequence sb = DOTween.Sequence();
            sb.Append(DOVirtual.Float(animator.GetFloat("Blend"), 1f, 0.2f, v => animator.SetFloat("Blend", v)));

            if (cooldown)
            {
                OnHit?.Invoke(damage);
                cooldown = false;
                UniTask.Delay(cooldownMs).ContinueWith(() => cooldown = true);
            }
            //  animator.SetFloat("Blend", 1);

        }
        else SetAnimRound();



    }


    public void SetAnimRound()
    {
        Sequence sb = DOTween.Sequence();
        sb.Append(DOVirtual.Float(animator.GetFloat("Blend"), 0.5f, 0.2f, v => animator.SetFloat("Blend", v)));
   //     animator.SetFloat("Blend", 0.5f);
    }
}
