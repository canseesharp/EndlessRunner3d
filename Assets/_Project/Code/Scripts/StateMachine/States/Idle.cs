using UnityEngine;

public class Idle : PlayerState
{
    public Idle(CharacterController characterController,
            PlayerAnimator animator,
            PlayerData data)
        : base(characterController, animator, data)
    {
    }
}
