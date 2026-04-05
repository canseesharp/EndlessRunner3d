using System.Collections.Generic;
using UnityEngine;
using EndlessRunner3d.SO;

namespace EndlessRunner3d.StateMachine.States
{
    public class Shifting : PlayerState
    {
        private readonly Dictionary<Line, float> _linesPosition = new();
        private readonly Transform _transform;
        private Line _targetLine;

        private float _progress;
        private float _elapsedSeconds;
        private float _startPositionX;

        public Shifting(PlayerController controller,
                PlayerAnimator animator,
                PlayerData data)
            : base(controller, animator, data)
        {
            _transform = controller.transform;
            _linesPosition[Line.Left] = -3f;
            _linesPosition[Line.Middle] = 0f;
            _linesPosition[Line.Right] = 3f;
        }

        public override void Enter()
        {
            _targetLine = Data.Line;
            _startPositionX = _transform.position.x;
            if (_startPositionX < _linesPosition[_targetLine])
            {
                Animator.PlayShiftRight();
            }
            else
            {
                Animator.PlayShiftLeft();
            }
        }

        public override void Update()
        {
            if (_progress > 1f)
            {
                IsPerformed = true;
                return;
            }

            _elapsedSeconds += Time.deltaTime;
            _progress = _elapsedSeconds / Data.ShiftDuration;

            float x = _startPositionX + (_linesPosition[_targetLine] - _startPositionX) * Data.ShiftCurve.Evaluate(_progress);
            PlayerController.FrameMotion.AddMotion(Vector3.right * (x - _transform.position.x));
        }

        public override void Exit()
        {
            IsPerformed = false;
            _progress = 0f;
            _elapsedSeconds = 0f;
            Animator.TryPlayRunAfterShift();
        }
    }
}
