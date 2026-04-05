using UnityEngine;

namespace EndlessRunner3d.StateMachine
{
    public class TimeFlagPredicate : IPredicate
    {
        private readonly float _time;

        private float _setTime;
        private bool _flag;

        public TimeFlagPredicate(float time)
        {
            _time = time;
        }

        public void SetFlag()
        {
            _flag = true;
            _setTime = Time.time;
        }

        public bool Evaluate()
        {
            if (_flag == true && Time.time - _setTime <= _time)
            {
                _flag = false;
                return true;
            }

            return false;
        }
    }
}
