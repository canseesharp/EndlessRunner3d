using UnityEngine;

public class Jumping : PlayerState
{
    private readonly Transform _transform;

    private float _progress;
    private float _startPositionY;
    private float _elapsedSeconds = 0f;

    public Jumping(PlayerController controller,
            PlayerAnimator animator,
            PlayerData data)
        : base(controller, animator, data)
    {
        _transform = controller.transform;
    }

    public override void Enter()
    {
        Animator.PlayJump();
        _startPositionY = _transform.position.y;
        PlayerController.DisableGravity();
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
        PlayerController.Move(Vector3.up * (y - _transform.position.y));
    }

    public override void Exit()
    {
        IsPerformed = false;
        _progress = 0f;
        _elapsedSeconds = 0f;
        PlayerController.EnableGravity();
    }
}
