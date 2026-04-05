using UnityEngine;

namespace EndlessRunner3d
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Despawner _despawner;
        [SerializeField] private GameDifficulty _gameDifficulty;
        [SerializeField] private WorldSection _startSectionTemplate;
        [SerializeField] private SectionPool[] _sectionPools;

        private readonly float _sectionLength = 36f;
        private readonly int _visibleSectionCount = 5;

        private void Awake()
        {
            foreach (var pool in _sectionPools)
            {
                pool.Init(_gameDifficulty);
            }
        }

        private void Start()
        {
            for (int i = 0; i < _visibleSectionCount; i++)
            {
                WorldSection instance = Instantiate(_startSectionTemplate, 
                        new Vector3(0f, 0f, _sectionLength * (i - 1)), 
                        Quaternion.identity);
                instance.Init(null, _gameDifficulty);
            }
        }

        private void OnEnable()
        {
            _despawner.SectionDespawned += OnSectionDespawned;
        }

        private void OnDisable()
        {
            _despawner.SectionDespawned -= OnSectionDespawned;
        }

        private void OnSectionDespawned(WorldSection section)
        {
            int poolNumber = Random.Range(0, _sectionPools.Length);
            WorldSection nextSection = _sectionPools[poolNumber].Get();
            nextSection.gameObject.SetActive(true);
            nextSection.transform.position =
                section.transform.position + new Vector3(0f, 0f, _sectionLength * _visibleSectionCount);
        }
    }
}
