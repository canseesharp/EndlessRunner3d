using System;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    public event Action<WorldSection> SectionDespawned;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WorldSection section))
        {
            section.ReleaseToPool();
            SectionDespawned?.Invoke(section);
        }
    }
}
