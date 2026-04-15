using UnityEngine;
using EndlessRunner3d.SO;

namespace EndlessRunner3d.StateMachine.States.Player
{
    public class Sliding : PlayerState
    {
        private float _elapsedSeconds;

        public Sliding(PlayerController controller,
                PlayerAnimator animator,
                PlayerData data)
            : base(controller, animator, data)
        {
        }

        public override void Enter()
        {
            PlayerController.Slide();
            Animator.PlaySlide();
        }

        public override void Update()
        {
            _elapsedSeconds += Time.deltaTime;

            if (_elapsedSeconds >= Data.SlideDuration)
            {
                IsPerformed = true;
            }
        }
    
        public override void Exit()
        {
            PlayerController.Stand();
            _elapsedSeconds = 0f;
            IsPerformed = false;
        }
    }
}
