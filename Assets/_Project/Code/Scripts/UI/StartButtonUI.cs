using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace EndlessRunner3d
{
    public class StartButtonUI : ButtonUI
    {
        [SerializeField] private GameObject _scorePanel;
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private CinemachineCamera _gameCamera;
        [SerializeField] private Player _player;

        [Inject] private GameDifficulty _gameDifficulty;

        protected override void OnButtonClick()
        {
            _scorePanel.SetActive(true);
            _mainMenu.SetActive(false);
            _gameCamera.Priority = 2;
            _gameDifficulty.OnGameStart();
            _player.OnGameStart();
        }
    }
}
