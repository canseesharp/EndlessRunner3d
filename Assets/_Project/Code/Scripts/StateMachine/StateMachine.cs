using System;
using System.Collections.Generic;

namespace EndlessRunner3d.StateMachine
{
    public class StateMachine
    {
        private StateNode _current;
        private Dictionary<Type, StateNode> _nodes = new();
        private HashSet<ITransition> _anyTransitions = new();

        public IState Current => _current.State;

        public StateMachine(IState initialState)
        {
            _current = GetOrAddNode(initialState);
            _current.State.Enter();
        }

        public void Update()
        {
            if (NeedTransit(out IState state))
            {
                ChangeState(state);
            }

            _current.State?.Update();
        }

        public void FixedUpdate()
        {
            _current.State?.FixedUpdate();
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        private void ChangeState(IState state)
        {
            if (_current.State == state)
            {
                return;
            }

            if (_nodes.TryGetValue(state.GetType(), out StateNode nextState))
            {
                _current.State?.Exit();
                _current = nextState;
                _current.State?.Enter();
            }
        }

        private bool NeedTransit(out IState state)
        {
            state = null;

            foreach (var transition in _anyTransitions)
            {
                if (transition.Condition.Evaluate())
                {
                    state = transition.To;
                    return true;
                }
            }

            foreach (var transition in _current.Transitions)
            {
                if (transition.Condition.Evaluate())
                {
                    state = transition.To;
                    return true;
                }
            }

            return false;
        }

        private StateNode GetOrAddNode(IState state)
        {
            var type = state.GetType();
            if (_nodes.TryGetValue(type, out StateNode node))
            {
                return node;
            }

            var newNode = new StateNode(state);
            _nodes.Add(type, newNode);

            return newNode;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}
