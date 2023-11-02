namespace Test.FSM.States
{
    public interface IGameState
    {
        IGameStateMachine FSM { get; }
        void Enter();
        void Exit();
    }
}