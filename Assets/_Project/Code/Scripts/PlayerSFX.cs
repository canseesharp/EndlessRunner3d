using UnityEngine;

namespace EndlessRunner3d
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerSFX : MonoBehaviour
    {
        [SerializeField] private AudioClip _hitSound;
        [SerializeField] private AudioClip[] _moveSounds;
        [SerializeField] private AudioClip[] _footSteps;

        private AudioSource _audioSource;
        private readonly float _hitVolume = 1f;
        private readonly float _moveVolume = 0.2f;
        private readonly float _footstepVolume = 1f;

        public void PlayHit()
        {
            _audioSource.PlayOneShot(_hitSound, _hitVolume);
        }

        public void PlayMove()
        {
            var sound = _moveSounds[Random.Range(0, _moveSounds.Length)];
            _audioSource.PlayOneShot(sound, _moveVolume);
        }

        public void PlayFootstep()
        {
            var sound = _footSteps[Random.Range(0, _footSteps.Length)];
            _audioSource.PlayOneShot(sound, _footstepVolume);
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
}
