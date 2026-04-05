using EndlessRunner3d.SO;

namespace EndlessRunner3d.StateMachine.States
{
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
}
