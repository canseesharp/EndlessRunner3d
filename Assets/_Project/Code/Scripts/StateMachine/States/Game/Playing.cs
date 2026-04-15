namespace EndlessRunner3d.StateMachine.States.Game
{
    public class Playing : GameState
    {
        public Playing(GameDifficulty gameDifficulty) : base(gameDifficulty)
        {
        }

        public override void Enter()
        {
            GameDifficulty.enabled = true;
        }
    }
}
