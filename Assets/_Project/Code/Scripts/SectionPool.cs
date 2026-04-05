using UnityEngine;
using UnityEngine.Pool;

namespace EndlessRunner3d
{
    public class SectionPool : MonoBehaviour
    {
        [SerializeField] private WorldSection _template;

        private ObjectPool<WorldSection> _objectPool;
        private readonly int _defaultSize = 3;
        private readonly int _maxSize = 10;

        public WorldSection Get() => _objectPool.Get();

        public void Init(GameDifficulty difficulty)
        {
            _objectPool = new ObjectPool<WorldSection>(
                    createFunc: () => CreateSection(difficulty), 
                    defaultCapacity: _defaultSize,
                    maxSize: _maxSize);
        }

        private WorldSection CreateSection(GameDifficulty difficulty)
        {
            WorldSection instance = Instantiate(_template, Vector3.zero, Quaternion.identity);
            instance.Init(_objectPool, difficulty);
            return instance;
        }
    }
}
