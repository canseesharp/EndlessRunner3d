using UnityEngine;

namespace EndlessRunner3d.SO
{
    [CreateAssetMenu(fileName = "SectionData", menuName = "Scriptable Objects/SectionData")]
    public class SectionData : ScriptableObject
    {
        [SerializeField] private float _speed;

        public float Speed => _speed;
    }
}
