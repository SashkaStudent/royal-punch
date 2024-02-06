using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HeroInstaller : MonoInstaller
{
    [SerializeField]
    ComboCounter combo;
    [SerializeField]
    Fight fight;

    [SerializeField]
    EnemyHealth enemyHealth;
    [SerializeField]
    HeroHealth heroHealth;

    [SerializeField]
    EnemyFight enemyFight;

    [SerializeField]
    EnemySpell enemySpell;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Fight>().FromInstance(fight).AsSingle();
        Container.Bind<EnemyHealth>().FromInstance(enemyHealth).AsSingle().NonLazy();
        Container.Bind<EnemyFight>().FromInstance(enemyFight).AsSingle().NonLazy();
        Container.Bind<EnemySpell>().FromInstance(enemySpell).AsSingle().NonLazy();
        Container.Bind<HeroHealth>().FromInstance(heroHealth).AsSingle().NonLazy();
        Container.Bind<ComboCounter>().FromInstance(combo).AsSingle().NonLazy();
    }
}
