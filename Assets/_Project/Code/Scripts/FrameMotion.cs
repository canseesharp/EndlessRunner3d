using UnityEngine;

namespace EndlessRunner3d
{
    public class FrameMotion
    {
        private Vector3 _motion;

        public void AddMotion(Vector3 motion)
        {
            _motion += motion;
        }

        public void ApplyMotionTo(CharacterController characterController)
        {
            characterController.Move(_motion);
            _motion = Vector3.zero;
        }
    }
}
