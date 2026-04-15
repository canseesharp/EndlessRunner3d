namespace EndlessRunner3d.StateMachine
{
    public abstract class GameState : IState
    {
        protected readonly GameDifficulty GameDifficulty;

        public bool IsPerformed { get; protected set; }

        public GameState(GameDifficulty gameDifficulty)
        {
            GameDifficulty = gameDifficulty;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Update()
        {
        }
    }
}
