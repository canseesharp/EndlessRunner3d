namespace EndlessRunner3d.StateMachine.States.Game
{
    public class GameOver : GameState
    {
        public GameOver(GameDifficulty gameDifficulty) : base(gameDifficulty)
        {
        }

        public override void Enter()
        {
            GameDifficulty.enabled = false;
        }
    }
}
