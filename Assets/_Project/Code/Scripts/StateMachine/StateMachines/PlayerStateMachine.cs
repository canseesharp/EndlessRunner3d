using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : MonoBehaviour
{
    private PlayerAnimator _animator;
    private PlayerData _data;
    private PlayerGravity _gravity;
    private StateMachine _verticalMachine;
    private StateMachine _horizontalMachine;

    private TimeFlagPredicate _jumpPredicate;
    private TimeFlagPredicate _shiftPredicate;
    private TimeFlagPredicate _slidePredicate;

    public void Init(PlayerData data, PlayerAnimator animator, PlayerGravity gravity)
    {
        _animator = animator;
        _data = data;
        _gravity = gravity;
        _jumpPredicate = new TimeFlagPredicate(data.JumpBuffer);
        _shiftPredicate = new TimeFlagPredicate(data.ShiftBuffer);
        _slidePredicate = new TimeFlagPredicate(data.SlideBuffer);
    }

    public void OnJumpButtonPressed()
    {
        _jumpPredicate.SetFlag();
    }

    public void OnShiftLeftButtonPressed()
    {
        if (_data.TryShiftLeft())
        {
            _shiftPredicate.SetFlag();
        }
    }

    public void OnShiftRightButtonPressed()
    {
        if (_data.TryShiftRight())
        {
            _shiftPredicate.SetFlag();
        }
    }

    public void OnSlideButtonPressed()
    {
        _slidePredicate.SetFlag();
    }

    private void Awake()
    {
        SetupStateMachine();
    }

    private void Update()
    {
        _verticalMachine.Update();
        _horizontalMachine.Update();
    }

    private void FixedUpdate()
    {
        _verticalMachine.FixedUpdate();
        _horizontalMachine.FixedUpdate();
    }

    private void SetupStateMachine()
    {
        var characterController = GetComponent<CharacterController>();
        var idle = new Idle(characterController, _animator, _data);
        var running = new Running(characterController, _animator, _data);
        _verticalMachine = new(running);
        var jumping = new Jumping(characterController, _animator, _data, _gravity);
        var falling = new Falling(characterController, _animator, _data);
        var shifting = new Shifting(characterController, _animator, _data);
        var sliding = new Sliding(characterController, _animator, _data);
        _horizontalMachine = new(idle);

        _verticalMachine.AddTransition(running, jumping, _jumpPredicate);
        _verticalMachine.AddTransition(jumping, falling, new FuncPredicate(() => jumping.IsPerformed == true));
        _verticalMachine.AddTransition(jumping, running, new FuncPredicate(() => _gravity.IsGrounded == true && jumping.IsPerformed == true));
        _verticalMachine.AddTransition(falling, running, new FuncPredicate(() => _gravity.IsGrounded == true));
        _verticalMachine.AddTransition(running, falling, new FuncPredicate(() => _gravity.IsGrounded == false));

        _verticalMachine.AddTransition(running, sliding, _slidePredicate);
        _verticalMachine.AddTransition(sliding, running, new FuncPredicate(() => sliding.IsPerformed == true));

        _horizontalMachine.AddTransition(idle, shifting, _shiftPredicate);
        _horizontalMachine.AddTransition(shifting, idle, new FuncPredicate(() => shifting.IsPerformed == true));
    }
}
