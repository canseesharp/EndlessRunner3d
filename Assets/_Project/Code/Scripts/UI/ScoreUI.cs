using TMPro;
using UnityEngine;

namespace EndlessRunner3d.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private TMP_Text _scoreText;

        private readonly string _format = "0";

        private void OnEnable()
        {
            _score.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _score.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(float score)
        {
            _scoreText.text = score.ToString(_format);
        }
    }
}
