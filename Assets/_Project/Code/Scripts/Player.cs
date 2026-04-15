using UnityEngine;
using EndlessRunner3d.StateMachine.Machines;
using Zenject;

namespace EndlessRunner3d
{
    [RequireComponent(typeof(PlayerStateMachine))]
    public class Player : MonoBehaviour
    {
        [Inject] private IGameStarter _gameStarter;

        private PlayerStateMachine _stateMachine;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _stateMachine = GetComponent<PlayerStateMachine>();
            _playerInput = new();
        }

        private void OnEnable()
        {
            _playerInput.Jumped += _stateMachine.OnJumpButtonPressed;
            _playerInput.Slided += _stateMachine.OnSlideButtonPressed;
            _playerInput.MovedLeft += _stateMachine.OnShiftLeftButtonPressed;
            _playerInput.MovedRight += _stateMachine.OnShiftRightButtonPressed;
            _gameStarter.GameStarted += OnGameStart;
            if (_gameStarter.IsStarted == true)
            {
                _playerInput.Enable();
            }
        }

        private void OnDisable()
        {
            _playerInput.Jumped -= _stateMachine.OnJumpButtonPressed;
            _playerInput.Slided -= _stateMachine.OnSlideButtonPressed;
            _playerInput.MovedLeft -= _stateMachine.OnShiftLeftButtonPressed;
            _playerInput.MovedRight -= _stateMachine.OnShiftRightButtonPressed;
            _gameStarter.GameStarted -= OnGameStart;
            _playerInput.Disable();
        }

        private void OnGameStart()
        {
            _gameStarter.GameStarted -= OnGameStart;
            _playerInput.Enable();
        }
    }
}
