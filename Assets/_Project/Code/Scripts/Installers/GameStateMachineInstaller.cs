using EndlessRunner3d.StateMachine.Machines;
using UnityEngine;
using Zenject;

public class GameStateMachineInstaller : MonoInstaller
{
    [SerializeField] private GameStateMachine _gameStateMachine;
    public override void InstallBindings()
    {
        Container.Bind<GameStateMachine>().FromInstance(_gameStateMachine).AsSingle();
    }
}
