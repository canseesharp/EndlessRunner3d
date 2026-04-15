using UnityEngine;

namespace EndlessRunner3d
{
    public class GameSettings : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 1;
        }
    }
}
