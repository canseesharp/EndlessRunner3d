public interface IState
{
    public bool IsPerformed { get; }

    void Enter();

    void Update();

    void FixedUpdate();

    void Exit();
}
