using UnityEngine;

namespace EndlessRunner3d
{
    public class CapsuleDimensions
    {
        private readonly Vector3 _center;
        private readonly float _height;

        public CapsuleDimensions(Vector3 center, float height)
        {
            _center = center;
            _height = height;
        }

        public void ApplyDimensionsTo(CharacterController characterController)
        {
            characterController.center = _center;
            characterController.height = _height;
        }
    }
}
