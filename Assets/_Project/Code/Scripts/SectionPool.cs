using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace EndlessRunner3d
{
    public class SectionPool : MonoBehaviour
    {
        [SerializeField] private WorldSection _template;

        [Inject] private GameDifficulty _gameDifficulty;

        private ObjectPool<WorldSection> _objectPool;
        private readonly int _defaultSize = 3;
        private readonly int _maxSize = 10;

        public WorldSection Get() => _objectPool.Get();

        private void Awake()
        {
            _objectPool = new ObjectPool<WorldSection>(
                    createFunc: CreateSection, 
                    defaultCapacity: _defaultSize,
                    maxSize: _maxSize);
        }

        private WorldSection CreateSection()
        {
            WorldSection instance = Instantiate(_template, Vector3.zero, Quaternion.identity);
            instance.Init(_objectPool, _gameDifficulty);
            instance.gameObject.SetActive(false);
            return instance;
        }
    }
}
