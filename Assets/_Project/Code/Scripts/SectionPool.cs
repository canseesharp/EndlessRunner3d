using UnityEngine;
using UnityEngine.Pool;

public class SectionPool : MonoBehaviour
{
    [SerializeField] private WorldSection _template;

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
        instance.Init(_objectPool);
        return instance;
    }
}
