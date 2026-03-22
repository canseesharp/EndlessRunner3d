using UnityEngine;

public abstract class PlayerState : IState
{
    protected readonly CharacterController CharacterController;
    protected readonly PlayerAnimator Animator;
    protected readonly PlayerData Data;

    protected PlayerState(CharacterController characterController, PlayerAnimator animator, PlayerData data)
    {
        CharacterController = characterController;
        Animator = animator;
        Data = data;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Update()
    {
    }
}
