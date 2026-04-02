using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameDifficulty _difficulty;
    [SerializeField] private SectionData _sectionData;
    [SerializeField] private Transform _headPosition;
    [SerializeField] private Transform _slidingHeadPosition;

    private PlayerData _data;
    private CharacterController _characterController;
    private readonly float _vectorEqualityFactor = 0.01f;
    private readonly float _slightForwardMove = 0.01f;
    private readonly float _rayLength = 0.5f;
    private float _coyoteTime;
    private float _groundedGravity;
    private bool _hasGravity = true;
    private Vector3 _surfaceNormal = Vector3.up;
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
        MoveAlongSurface();
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
            _surfaceNormal = Vector3.up;
        }
    }

    private void ApplyGravity()
    {
        if (_hasGravity == true && Vector3.Distance(_surfaceNormal, Vector3.up) < _vectorEqualityFactor)
        {
            float gravity = IsGrounded == true ? _groundedGravity : _data.FallSpeed;
            FrameMotion.AddMotion(Vector3.down * gravity * Time.deltaTime);
        }
    }

    private Vector3 Project(Vector3 direction) => direction - Vector3.Dot(direction, _surfaceNormal) * _surfaceNormal;

    private void MoveAlongSurface()
    {
        var directionAlongSurface = Project(Vector3.forward);
        FrameMotion.AddMotion(directionAlongSurface * ((_sectionData.Speed * _difficulty.Multiplier + _slightForwardMove) * Time.deltaTime));
    }

    private void ClampZ() => transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitDirection = hit.moveDirection;
        if (hitDirection == Vector3.down)
        {
            _surfaceNormal = hit.normal;
        }
        else if (Physics.Raycast(_collisionCheckPoint.position, Vector3.forward, _rayLength)
                || Physics.Raycast(_collisionCheckPoint.position, Vector3.right, _rayLength)
                || Physics.Raycast(_collisionCheckPoint.position, Vector3.left, _rayLength))
        {
            _difficulty.OnDead();
            ObstacleHit?.Invoke();
        }
    }
}
