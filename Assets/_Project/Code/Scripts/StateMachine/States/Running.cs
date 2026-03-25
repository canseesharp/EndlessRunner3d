using UnityEngine;

public class Running : PlayerState
{
    public Running(CharacterController characterController,
            PlayerAnimator animator,
            PlayerData data)
        : base(characterController, animator, data)
    {
    }

    public override void Enter()
    {
        Animator.PlayRun();
    }
}
