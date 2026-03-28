using UnityEngine;
using UnityEngine.Pool;

public class WorldSection : MonoBehaviour
{
    [SerializeField] private SectionData _data;
    private IObjectPool<WorldSection> _poolContainer;

    public void Init(IObjectPool<WorldSection> pool)
    {
        _poolContainer = pool;
    }

    public void ReleaseToPool()
    {
        gameObject.SetActive(false);
        _poolContainer?.Release(this);
    }

    private void Update()
    {
        transform.Translate(Vector3.back * (_data.Speed * Time.deltaTime));
    }
}
