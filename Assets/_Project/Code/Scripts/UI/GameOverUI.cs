using TMPro;
using UnityEngine;

namespace EndlessRunner3d
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private PlayerController _playerController;

        private readonly string _format = "0";

        private void OnEnable()
        {
            _playerController.ObstacleHit += OnObstacleHit;
        }

        private void OnDisable()
        {
            _playerController.ObstacleHit -= OnObstacleHit;
        }

        private void OnObstacleHit()
        {
            _scoreText.text = _score.Value.ToString(_format);
            _gameOverPanel.SetActive(true);
        }
    }
}
