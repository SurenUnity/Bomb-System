using Controllers;
using UnityEngine;
using Zenject;
using CharacterController = Controllers.CharacterController;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CharacterController>().AsSingle().NonLazy();
        Container.Bind<BombController>().AsSingle().NonLazy();
    }
}
