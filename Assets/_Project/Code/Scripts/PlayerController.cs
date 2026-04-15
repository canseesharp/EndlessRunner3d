using System;
using UnityEngine;
using EndlessRunner3d.SO;
using Zenject;

namespace EndlessRunner3d
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Transform _headBone;

        [Inject] private GameDifficulty _gameDifficulty;
        [Inject] private PlayerData _data;

        private CharacterController _characterController;
        private CapsuleDimensions _standCapsule;
        private CapsuleDimensions _slideCapsule;
        private readonly float _collisionRadius = 0.3f;
        private float _coyoteTime;

        public bool IsGrounded => _coyoteTime > 0f;
        public FrameMotion FrameMotion { get; private set; }
        public bool HasGravity { get; set; } = true;

        public event Action ObstacleHit;

        public void Slide() => _slideCapsule.ApplyDimensionsTo(_characterController);

        public void Stand() => _standCapsule.ApplyDimensionsTo(_characterController);

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _standCapsule = new CapsuleDimensions(_characterController.center, _characterController.height);
            _slideCapsule = new CapsuleDimensions(new Vector3(_characterController.center.x,
                    _characterController.center.y * 0.5f,
                    _characterController.center.z),
                    _characterController.height * 0.5f);
            FrameMotion = new();
        }

        private void Update()
        {
            CheckGround();
            ApplyGravity();
            MoveForward();
            FrameMotion.ApplyMotionTo(_characterController);
            ClampZ();
        }

        private void CheckGround()
        {
            if (_characterController.isGrounded)
            {
                _coyoteTime = _data.CoyoteTime;
            }
            else
            {
                _coyoteTime -= Time.deltaTime;
            }
        }

        private void ApplyGravity()
        {
            if (HasGravity == true)
            {
                float gravity = IsGrounded == true ? _data.GroundedGravity : _data.FallSpeed;
                FrameMotion.AddMotion(Vector3.down * gravity * Time.deltaTime);
            }
        }

        private void MoveForward() => FrameMotion.AddMotion(Vector3.forward * (_gameDifficulty.WorldSpeed * Time.deltaTime));

        private void ClampZ() => transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.moveDirection != Vector3.down && Physics.CheckSphere(_headBone.position, _collisionRadius, ~_playerLayer))
            {
                _gameDifficulty.OnDead();
                ObstacleHit?.Invoke();
            }
        }
    }
}
