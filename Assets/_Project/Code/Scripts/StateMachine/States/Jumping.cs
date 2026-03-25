using UnityEngine;

public class Jumping : PlayerState
{
    private readonly Transform _transform;
    private readonly PlayerGravity _gravity;

    private float _progress;
    private float _startPositionY;
    private float _elapsedSeconds = 0f;

    public Jumping(CharacterController characterController,
            PlayerAnimator animator,
            PlayerData data,
            PlayerGravity gravity)
        : base(characterController, animator, data)
    {
        _transform = characterController.transform;
        _gravity = gravity;
    }

    public override void Enter()
    {
        Animator.PlayJump();
        _startPositionY = _transform.position.y;
        _gravity.DisableGravity();
    }

    public override void Update()
    {
        if (_progress > 1f)
        {
            IsPerformed = true;
            return;
        }

        _elapsedSeconds += Time.deltaTime;
        _progress = _elapsedSeconds / Data.JumpDuration;
        float y = _startPositionY + Data.JumpCurve.Evaluate(_progress) * Data.JumpHeight;
        CharacterController.Move(Vector3.up * (y - _transform.position.y));
    }

    public override void Exit()
    {
        IsPerformed = false;
        _progress = 0f;
        _elapsedSeconds = 0f;
        _gravity.EnableGravity();
    }
}
