using System;
using EndlessRunner3d.StateMachine.States.Game;
using UnityEngine;
using Zenject;

namespace EndlessRunner3d.StateMachine.Machines
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        [Inject] private GameDifficulty _gameDifficulty;
        [Inject] private IGameStarter _gameStarter;

        private StateMachine _stateMachine;
        private bool _obstacleHited;

        public event Action<IState> StateChanged;

        private void Awake()
        {
            SetupStateMachine();
        }

        private void OnEnable()
        {
            _playerController.ObstacleHit += OnObstacleHit;
            _stateMachine.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _playerController.ObstacleHit -= OnObstacleHit;
            _stateMachine.StateChanged -= OnStateChanged;
        }

        private void OnObstacleHit() => _obstacleHited = true;

        private void OnStateChanged(IState state) => StateChanged?.Invoke(state);

        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        private void SetupStateMachine()
        {
            var idle = new Idle(_gameDifficulty);
            var playing = new Playing(_gameDifficulty);
            var gameOver = new GameOver(_gameDifficulty);
            _stateMachine = new StateMachine(idle);

            _stateMachine.AddTransition(idle, playing, new FuncPredicate(() => _gameStarter.IsStarted == true));
            _stateMachine.AddTransition(playing, gameOver, new FuncPredicate(() => _obstacleHited == true));
        }
    }
}
