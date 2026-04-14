using UnityEngine;

namespace EndlessRunner3d
{
    public class GameDifficulty : MonoBehaviour
    {
        [SerializeField] private float _maxMultiplier;
        [SerializeField] private float _secondsToReachMax;
        [SerializeField] private AnimationCurve _difficultyCurve;
        [SerializeField] private float _baseWorldSpeed;

        private float _elapsedSeconds;
        private float _progress;

        public float Multiplier { get; private set; }

        public float WorldSpeed => _baseWorldSpeed * Multiplier;

        public void OnGameStart() => enabled = true;

        public void OnDead()
        {
            Multiplier = 0f;
            enabled = false;
        }

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (_progress >= 1f)
            {
                return;
            }
            _elapsedSeconds += Time.deltaTime;
            _progress = _elapsedSeconds / _secondsToReachMax;

            Multiplier = _difficultyCurve.Evaluate(_progress) * _maxMultiplier;
        }
    }
}
