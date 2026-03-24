using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerGravity : MonoBehaviour
{
    private PlayerData _data;
    private CharacterController _characterController;

    private float _coyoteTime;
    private float _groundedGravity;
    private bool _hasGravity = true;

    public bool IsGrounded => _coyoteTime > 0f;

    public void Init(PlayerData data)
    {
        _data = data;
        _groundedGravity = _data.FallSpeed * 0.2f;
    }

    public void EnableGravity() => _hasGravity = true;

    public void DisableGravity() => _hasGravity = false;

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
        }

        if (_hasGravity == true)
        {
            float gravity = IsGrounded == true ? _groundedGravity : _data.FallSpeed;
            _characterController.Move(Vector3.down * gravity * Time.deltaTime);
        }
    }
}
