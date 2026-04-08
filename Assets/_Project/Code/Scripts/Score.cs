using System;
using UnityEngine;

namespace EndlessRunner3d
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private GameDifficulty _difficulty;

        private readonly float _baseMultiplier = 2f;
        private float _score;

        public event Action<float> ScoreChanged;

        private void Update()
        {
            _score += _baseMultiplier * _difficulty.Multiplier * Time.deltaTime;
            ScoreChanged?.Invoke(_score);
        }
    }
}
