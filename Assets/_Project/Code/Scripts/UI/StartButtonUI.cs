using System;
using Unity.Cinemachine;
using UnityEngine;

namespace EndlessRunner3d.UI
{
    public class StartButtonUI : ButtonUI, IGameStarter
    {
        [SerializeField] private GameObject _scorePanel;
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private CinemachineCamera _gameCamera;

        public bool IsStarted { get; private set; }

        public event Action GameStarted;

        protected override void OnButtonClick()
        {
            _scorePanel.SetActive(true);
            _mainMenu.SetActive(false);
            _gameCamera.Priority = 2;
            IsStarted = true;
            GameStarted?.Invoke();
        }
    }
}
