using UnityEngine;
using EndlessRunner3d.SO;
using EndlessRunner3d.StateMachine.States;

namespace EndlessRunner3d.StateMachine.Machines
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private PlayerData _data;
        private PlayerAnimator _animator;
        private PlayerController _controller;
        private StateMachine _verticalMachine;
        private StateMachine _horizontalMachine;

        private TimeFlagPredicate _jumpPredicate;
        private TimeFlagPredicate _shiftPredicate;
        private TimeFlagPredicate _slidePredicate;
        private bool _isDead;

        public void Init(PlayerData data, PlayerAnimator animator, PlayerController controller)
        {
            _animator = animator;
            _data = data;
            _controller = controller;
            _jumpPredicate = new TimeFlagPredicate(data.JumpBuffer);
            _shiftPredicate = new TimeFlagPredicate(data.ShiftBuffer);
            _slidePredicate = new TimeFlagPredicate(data.SlideBuffer);
        }

        public void OnJumpButtonPressed()
        {
            _jumpPredicate.SetFlag();
        }

        public void OnShiftLeftButtonPressed()
        {
            if (_horizontalMachine.Current is not Shifting && _data.TryShiftLeft())
            {
                _shiftPredicate.SetFlag();
            }
        }

        public void OnShiftRightButtonPressed()
        {
            if (_horizontalMachine.Current is not Shifting &&_data.TryShiftRight())
            {
                _shiftPredicate.SetFlag();
            }
        }

        public void OnSlideButtonPressed()
        {
            _slidePredicate.SetFlag();
        }

        private void Awake()
        {
            SetupStateMachine();
        }

        private void OnEnable()
        {
            _controller.ObstacleHit += OnObstacleHit;
        }

        private void OnDisable()
        {
            _controller.ObstacleHit -= OnObstacleHit;
        }

        private void Update()
        {
            _verticalMachine.Update();
            _horizontalMachine.Update();
        }

        private void FixedUpdate()
        {
            _verticalMachine.FixedUpdate();
            _horizontalMachine.FixedUpdate();
        }

        private void OnObstacleHit()
        {
            _isDead = true;
        }

        private void SetupStateMachine()
        {
            var idle = new Idle(_controller, _animator, _data);
            var running = new Running(_controller, _animator, _data);
            _verticalMachine = new(running);
            var jumping = new Jumping(_controller, _animator, _data);
            var falling = new Falling(_controller, _animator, _data);
            var shifting = new Shifting(_controller, _animator, _data);
            var sliding = new Sliding(_controller, _animator, _data);
            var dead = new Dead(_controller, _animator, _data);
            _horizontalMachine = new(idle);

            _verticalMachine.AddTransition(running, jumping, _jumpPredicate);
            _verticalMachine.AddTransition(jumping, falling, new FuncPredicate(() => jumping.IsPerformed == true));
            _verticalMachine.AddTransition(jumping, running, new FuncPredicate(() => _controller.IsGrounded == true && jumping.IsPerformed == true));
            _verticalMachine.AddTransition(falling, running, new FuncPredicate(() => _controller.IsGrounded == true));
            _verticalMachine.AddTransition(running, falling, new FuncPredicate(() => _controller.IsGrounded == false));

            _verticalMachine.AddTransition(running, sliding, _slidePredicate);
            _verticalMachine.AddTransition(sliding, running, new FuncPredicate(() => sliding.IsPerformed == true));

            _horizontalMachine.AddTransition(idle, shifting, _shiftPredicate);
            _horizontalMachine.AddTransition(shifting, idle, new FuncPredicate(() => shifting.IsPerformed == true));

            _verticalMachine.AddAnyTransition(dead, new FuncPredicate(() => _isDead == true));
            _horizontalMachine.AddAnyTransition(dead, new FuncPredicate(() => _isDead == true));
        }
    }
}
