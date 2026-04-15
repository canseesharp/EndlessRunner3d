using UnityEngine;
using EndlessRunner3d.StateMachine.Machines;

namespace EndlessRunner3d
{
    [RequireComponent(typeof(PlayerStateMachine), typeof(Animator), typeof(PlayerController))]
    public class Player : MonoBehaviour
    {
        private PlayerStateMachine _stateMachine;
        private PlayerAnimator _animator;
        private PlayerInput _playerInput;
        private bool _gameStarted;

        public void OnGameStart()
        {
            _gameStarted = true;
            _stateMachine.OnGameStart();
            _playerInput.Enable();
        }

        private void Awake()
        {
            var controller = GetComponent<PlayerController>();

            _animator = new(GetComponent<Animator>());

            _stateMachine = GetComponent<PlayerStateMachine>();
            _stateMachine.Init(_animator, controller);

            _playerInput = new();
        }

        private void OnEnable()
        {
            _playerInput.Jumped += _stateMachine.OnJumpButtonPressed;
            _playerInput.Slided += _stateMachine.OnSlideButtonPressed;
            _playerInput.MovedLeft += _stateMachine.OnShiftLeftButtonPressed;
            _playerInput.MovedRight += _stateMachine.OnShiftRightButtonPressed;
            if (_gameStarted == true)
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
            _playerInput.Disable();
        }
    }
}
