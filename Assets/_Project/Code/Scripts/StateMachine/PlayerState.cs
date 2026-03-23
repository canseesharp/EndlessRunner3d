using UnityEngine;

public abstract class PlayerState : IState
{
    protected readonly CharacterController CharacterController;
    protected readonly PlayerAnimator Animator;
    protected readonly PlayerData Data;

    public bool IsPerformed { get; protected set; }

    protected PlayerState(CharacterController characterController, PlayerAnimator animator, PlayerData data)
    {
        CharacterController = characterController;
        Animator = animator;
        Data = data;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}
