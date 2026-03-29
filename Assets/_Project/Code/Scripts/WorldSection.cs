using UnityEngine;
using UnityEngine.Pool;

public class WorldSection : MonoBehaviour
{
    [SerializeField] private SectionData _data;
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
        if (_gameDifficulty != null)
        {
            transform.Translate(Vector3.back * (_data.Speed * _gameDifficulty.Multiplier * Time.deltaTime));
        }
        else
        {
            transform.Translate(Vector3.back * (_data.Speed * Time.deltaTime));
        }
    }
}
