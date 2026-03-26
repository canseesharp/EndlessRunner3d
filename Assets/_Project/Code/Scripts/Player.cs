using UnityEngine;

[RequireComponent(typeof(PlayerStateMachine), typeof(Animator), typeof(PlayerGravity))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData _data;

    private PlayerStateMachine _stateMachine;
    private PlayerAnimator _animator;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _data.Init();

        var gravity = GetComponent<PlayerGravity>();
        gravity.Init(_data);

        _animator = new(GetComponent<Animator>());

        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Init(_data, _animator, gravity);

        _playerInput = new();
    }

    private void OnEnable()
    {
        _playerInput.Jumped += _stateMachine.OnJumpButtonPressed;
        _playerInput.Slided += _stateMachine.OnSlideButtonPressed;
        _playerInput.MovedLeft += _stateMachine.OnShiftLeftButtonPressed;
        _playerInput.MovedRight += _stateMachine.OnShiftRightButtonPressed;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Jumped -= _stateMachine.OnJumpButtonPressed;
        _playerInput.Slided -= _stateMachine.OnSlideButtonPressed;
        _playerInput.MovedLeft -= _stateMachine.OnShiftLeftButtonPressed;
        _playerInput.MovedRight -= _stateMachine.OnShiftRightButtonPressed;
        _playerInput.Disable();
    }
}
