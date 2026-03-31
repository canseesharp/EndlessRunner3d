using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameDifficulty _difficulty;
    [SerializeField] private SectionData _sectionData;

    private PlayerData _data;
    private CharacterController _characterController;

    private float _coyoteTime;
    private float _groundedGravity;
    private bool _hasGravity = true;
    private Vector3 _frameMotion = Vector3.zero;

    private readonly float _vectorEqualityFactor = 0.01f;
    private readonly float _slightForwardMove = 0.01f;
    private Vector3 _surfaceNormal = Vector3.up;

    public bool IsGrounded => _coyoteTime > 0f;

    public event Action ObstacleHit;

    public void Init(PlayerData data)
    {
        _data = data;
        _groundedGravity = _data.FallSpeed * 0.2f;
    }

    public void EnableGravity() => _hasGravity = true;

    public void DisableGravity() => _hasGravity = false;

    public void Move(Vector3 motion)
    {
        _frameMotion += motion;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
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

        if (_hasGravity == true && Vector3.Distance(_surfaceNormal, Vector3.up) < _vectorEqualityFactor)
        {
            float gravity = IsGrounded == true ? _groundedGravity : _data.FallSpeed;
            Move(Vector3.down * gravity * Time.deltaTime);
        }

        var project = Vector3.forward - Vector3.Dot(Vector3.forward, _surfaceNormal) * _surfaceNormal;
        Move(project * ((_sectionData.Speed * _difficulty.Multiplier + _slightForwardMove) * Time.deltaTime));
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        _characterController.Move(_frameMotion);
        _frameMotion = Vector3.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.moveDirection == Vector3.forward)
        {
            ObstacleHit?.Invoke();
        }
        else if (hit.moveDirection == Vector3.down)
        {
            _surfaceNormal = hit.normal;
        }
    }
}
