public abstract class PlayerState : IState
{
    protected readonly PlayerController PlayerController;
    protected readonly PlayerAnimator Animator;
    protected readonly PlayerData Data;

    public bool IsPerformed { get; protected set; }

    protected PlayerState(PlayerController controller, PlayerAnimator animator, PlayerData data)
    {
        PlayerController = controller;
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
