namespace EndlessRunner3d.StateMachine.States.Game
{
    public class Idle : GameState
    {
        public Idle(GameDifficulty gameDifficulty) : base(gameDifficulty)
        {
        }

        public override void Enter()
        {
            GameDifficulty.enabled = false;
        }
    }
}
