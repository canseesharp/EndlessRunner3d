using EndlessRunner3d;
using UnityEngine;
using Zenject;

public class GameStarterInstaller : MonoInstaller
{
    [SerializeField] private StartButtonUI _gameStarter;

    public override void InstallBindings()
    {
        Container.Bind<IGameStarter>().FromInstance(_gameStarter).AsSingle();
    }
}
