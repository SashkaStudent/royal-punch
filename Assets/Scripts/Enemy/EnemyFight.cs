using Cysharp.Threading.Tasks;
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

        if (playerFight.transform.position.magnitude < playerFight.MinDistance && dot < dotThreashold)
            animator.SetFloat("Blend", 1);
        else SetAnimRound();



    }


    public void SetAnimRound()
    {
        animator.SetFloat("Blend", 0.5f);
    }
}
