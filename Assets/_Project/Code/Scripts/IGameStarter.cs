using System;

namespace EndlessRunner3d
{
    public interface IGameStarter
    {
        bool IsStarted { get; }

        event Action GameStarted;
    }
}
