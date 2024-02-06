using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HeroHealth : Health
{
    [Inject]
    EnemyFight enemyFight;

    public event Action OnPunch; 

    protected override void Awake()
    {
        base.Awake();

        enemyFight.OnHit += HitHandler;
    }

    protected override void HitHandler(int value)
    {
        base.HitHandler(value);
    }


    public void Punch(int value)
    {
        DecreaseHealth(value);
        OnPunch?.Invoke();

    }

    protected override void OnDestroy()
    {

        enemyFight.OnHit -= HitHandler;

    }
}
