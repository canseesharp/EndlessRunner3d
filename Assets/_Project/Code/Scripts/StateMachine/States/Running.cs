public class Running : PlayerState
{
    public Running(PlayerController controller,
            PlayerAnimator animator,
            PlayerData data)
        : base(controller, animator, data)
    {
    }

    public override void Enter()
    {
        Animator.PlayRun();
    }
}
