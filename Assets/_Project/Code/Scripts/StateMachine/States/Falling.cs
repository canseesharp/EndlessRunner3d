using UnityEngine;

public class Falling : PlayerState
{
    public Falling(CharacterController characterController,
            PlayerAnimator animator,
            PlayerData data)
        : base(characterController, animator, data)
    {
    }

    public override void Enter()
    {
        Animator.PlayFalling();
    }
}
