using EndlessRunner3d.SO;
using UnityEngine;
using Zenject;

public class PlayerDataInstaller : MonoInstaller
{
    [SerializeField] private PlayerData _playerData;

    public override void InstallBindings()
    {
        _playerData.Init();
        Container.Bind<PlayerData>().FromInstance(_playerData).AsSingle();
    }
}
