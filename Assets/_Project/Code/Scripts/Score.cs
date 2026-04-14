using System;
using UnityEngine;
using Zenject;

namespace EndlessRunner3d
{
    public class Score : MonoBehaviour
    {
        [Inject] private GameDifficulty _gameDifficulty;

        private readonly float _baseMultiplier = 2f;
        private float _score;

        public float Value => _score;

        public event Action<float> ScoreChanged;

        private void Update()
        {
            _score += _baseMultiplier * _gameDifficulty.Multiplier * Time.deltaTime;
            ScoreChanged?.Invoke(_score);
        }
    }
}
