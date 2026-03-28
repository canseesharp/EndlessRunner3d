using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Despawner _despawner;
    [SerializeField] private SectionPool[] _sectionPools;

    private readonly float _sectionLength = 36f;
    private readonly int _visibleSectionCount = 3;

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
