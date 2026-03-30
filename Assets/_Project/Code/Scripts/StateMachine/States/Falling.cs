public class Falling : PlayerState
{
    public Falling(PlayerController controller,
            PlayerAnimator animator,
            PlayerData data)
        : base(controller, animator, data)
    {
    }

    public override void Enter()
    {
        Animator.PlayFalling();
    }
}
