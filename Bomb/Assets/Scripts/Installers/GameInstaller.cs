using Controllers;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CharactersController>().AsSingle().NonLazy();
        Container.Bind<BombsController>().AsSingle().NonLazy();
    }
}
