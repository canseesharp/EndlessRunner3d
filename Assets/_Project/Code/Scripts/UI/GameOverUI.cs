using EndlessRunner3d.StateMachine;
using EndlessRunner3d.StateMachine.Machines;
using EndlessRunner3d.StateMachine.States.Game;
using TMPro;
using UnityEngine;
using Zenject;

namespace EndlessRunner3d.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TMP_Text _scoreText;

        [Inject] private GameStateMachine _gameStateMachine;

        private readonly string _format = "0";

        private void OnEnable()
        {
            _gameStateMachine.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _gameStateMachine.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged(IState state)
        {
            if (state is GameOver)
            {
                _scoreText.text = _score.Value.ToString(_format);
                _gameOverPanel.SetActive(true);
            }
        }
    }
}
