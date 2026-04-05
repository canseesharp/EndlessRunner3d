using EndlessRunner3d.SO;

namespace EndlessRunner3d.StateMachine.States
{
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
}
