using UnityEngine;

public class Sliding : PlayerState
{
    private float _elapsedSeconds;

    public Sliding(CharacterController characterController,
            PlayerAnimator animator,
            PlayerData data)
        : base(characterController, animator, data)
    {
    }

    public override void Enter()
    {
        Animator.PlaySlide();
    }

    public override void Update()
    {
        _elapsedSeconds += Time.deltaTime;

        if (_elapsedSeconds >= Data.SlideDuration)
        {
            IsPerformed = true;
        }
    }
    
    public override void Exit()
    {
        _elapsedSeconds = 0f;
        IsPerformed = false;
    }
}
