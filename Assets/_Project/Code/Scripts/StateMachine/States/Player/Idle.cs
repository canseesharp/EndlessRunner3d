using EndlessRunner3d.SO;

namespace EndlessRunner3d.StateMachine.States.Player
{
    public class Idle : PlayerState
    {
        public Idle(PlayerController controller,
                PlayerAnimator animator,
                PlayerData data)
            : base(controller, animator, data)
        {
        }
    }
}
