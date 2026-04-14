using EndlessRunner3d;
using UnityEngine;
using Zenject;

public class GameDifficultyInstaller : MonoInstaller
{
    [SerializeField] private GameDifficulty _gameDifficulty;

    public override void InstallBindings()
    {
        Container.Bind<GameDifficulty>().FromInstance(_gameDifficulty).AsSingle();
    }
}
