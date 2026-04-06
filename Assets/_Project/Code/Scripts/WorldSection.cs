using UnityEngine;
using UnityEngine.Pool;

namespace EndlessRunner3d
{
    public class WorldSection : MonoBehaviour
    {
        private IObjectPool<WorldSection> _poolContainer;
        private GameDifficulty _gameDifficulty;

        public void Init(IObjectPool<WorldSection> pool, GameDifficulty difficulty)
        {
            _poolContainer = pool;
            _gameDifficulty = difficulty;
        }

        public void ReleaseToPool()
        {
            gameObject.SetActive(false);
            _poolContainer?.Release(this);
        }

        private void Update()
        {
            transform.Translate(Vector3.back * (_gameDifficulty.WorldSpeed * Time.deltaTime));
        }
    }
}
