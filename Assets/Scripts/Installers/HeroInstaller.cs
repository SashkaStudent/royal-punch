using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HeroInstaller : MonoInstaller
{
    [SerializeField]
    Fight fight;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Fight>().FromInstance(fight).AsSingle();
    }
}
