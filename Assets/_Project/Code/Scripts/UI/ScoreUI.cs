using EndlessRunner3d.StateMachine;
using EndlessRunner3d.StateMachine.Machines;
using EndlessRunner3d.StateMachine.States.Game;
using TMPro;
using UnityEngine;
using Zenject;

namespace EndlessRunner3d.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private GameObject _scorePanel;
        [SerializeField] private TMP_Text _scoreText;

        [Inject] private GameStateMachine _gameStateMachine;

        private readonly string _format = "0";

        private void OnEnable()
        {
            _score.ScoreChanged += OnScoreChanged;
            _gameStateMachine.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _score.ScoreChanged -= OnScoreChanged;
            _gameStateMachine.StateChanged -= OnStateChanged;
        }

        private void OnScoreChanged(float score)
        {
            _scoreText.text = score.ToString(_format);
        }

        private void OnStateChanged(IState state)
        {
            if (state is GameOver)
            {
                _scorePanel.SetActive(false);
            }
        }
    }
}
