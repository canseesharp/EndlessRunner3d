using System;
using UnityEngine;
using EndlessRunner3d.SO;

namespace EndlessRunner3d
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameDifficulty _difficulty;
        [SerializeField] private SectionData _sectionData;
        [SerializeField] private Transform _headPosition;
        [SerializeField] private Transform _slidingHeadPosition;

        private PlayerData _data;
        private CharacterController _characterController;
        private readonly float _rayLength = 0.5f;
        private float _coyoteTime;
        private float _groundedGravity;
        private bool _hasGravity = true;
        private CapsuleDimensions _standCapsule;
        private CapsuleDimensions _slideCapsule;
        private Transform _collisionCheckPoint;

        public bool IsGrounded => _coyoteTime > 0f;
        public FrameMotion FrameMotion { get; private set; }

        public event Action ObstacleHit;

        public void Init(PlayerData data)
        {
            _data = data;
            _groundedGravity = _data.FallSpeed * 0.2f;
        }

        public void EnableGravity() => _hasGravity = true;

        public void DisableGravity() => _hasGravity = false;

        public void Slide()
        {
            _slideCapsule.ApplyDimensionsTo(_characterController);
            _collisionCheckPoint = _slidingHeadPosition;
        }

        public void Stand()
        {
            _standCapsule.ApplyDimensionsTo(_characterController);
            _collisionCheckPoint = _headPosition;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _standCapsule = new CapsuleDimensions(_characterController.center, _characterController.height);
            _slideCapsule = new CapsuleDimensions(new Vector3(_characterController.center.x,
                    _characterController.center.y * 0.5f,
                    _characterController.center.z),
                    _characterController.height * 0.5f);
            FrameMotion = new();
            _collisionCheckPoint = _headPosition;
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
            if (_hasGravity == true)
            {
                float gravity = IsGrounded == true ? _groundedGravity : _data.FallSpeed;
                FrameMotion.AddMotion(Vector3.down * gravity * Time.deltaTime);
            }
        }

        private void MoveForward()
        {
            FrameMotion.AddMotion(Vector3.forward * (_sectionData.Speed * _difficulty.Multiplier * Time.deltaTime));
        }

        private void ClampZ() => transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.moveDirection != Vector3.down &&
                    (Physics.Raycast(_collisionCheckPoint.position, Vector3.forward, _rayLength)
                    || Physics.Raycast(_collisionCheckPoint.position, Vector3.right, _rayLength)
                    || Physics.Raycast(_collisionCheckPoint.position, Vector3.left, _rayLength)))
            {
                _difficulty.OnDead();
                ObstacleHit?.Invoke();
            }
        }
    }
}
