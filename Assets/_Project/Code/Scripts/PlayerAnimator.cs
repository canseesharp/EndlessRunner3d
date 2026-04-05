using UnityEngine;

namespace EndlessRunner3d
{
    public class PlayerAnimator
    {
        private readonly float _crossFadeDuration = 0.1f;
        private readonly Animator _animator;
        private int _currentAnimation;

        private readonly int _run = Animator.StringToHash("Run");
        private readonly int _jump = Animator.StringToHash("Jump");
        private readonly int _falling = Animator.StringToHash("Falling");
        private readonly int _slide = Animator.StringToHash("Slide");
        private readonly int _shiftLeft = Animator.StringToHash("ShiftLeft");
        private readonly int _shiftRight = Animator.StringToHash("ShiftRight");
        private readonly int _dead = Animator.StringToHash("Dead");

        public PlayerAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void PlayRun()
        {
            _animator.CrossFade(_run, _crossFadeDuration);
            _currentAnimation = _run;
        }

        public void PlayJump()
        {
            _animator.CrossFade(_jump, _crossFadeDuration);
            _currentAnimation = _jump;
        }

        public void PlayFalling()
        {
            _animator.CrossFade(_falling, _crossFadeDuration);
            _currentAnimation = _falling;
        }

        public void PlaySlide()
        {
            _animator.CrossFade(_slide, _crossFadeDuration);
            _currentAnimation = _slide;
        }

        public void PlayShiftLeft()
        {
            if (_currentAnimation == _run)
            {
                _animator.CrossFade(_shiftLeft, _crossFadeDuration);
                _currentAnimation = _shiftLeft;
            }
        }

        public void PlayShiftRight()
        {
            if (_currentAnimation == _run)
            {
                _animator.CrossFade(_shiftRight, _crossFadeDuration);
                _currentAnimation = _shiftRight;
            }
        }

        public void TryPlayRunAfterShift()
        {
            if (_currentAnimation == _shiftLeft || _currentAnimation == _shiftRight)
            {
                _animator.CrossFade(_run, _crossFadeDuration);
                _currentAnimation = _run;
            }
        }

        public void PlayDead()
        {
            _animator.CrossFade(_dead, _crossFadeDuration);
            _currentAnimation = _dead;
        }
    }
}
