using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HeroInstaller : MonoInstaller
{
    [SerializeField]
    Fight fight;

    [SerializeField]
    EnemyHealth enemyHealth;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Fight>().FromInstance(fight).AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyHealth>().FromInstance(enemyHealth).AsSingle();
    }
}
