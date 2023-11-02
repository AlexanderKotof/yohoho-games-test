using Test.FSM.States;
using VContainer.Unity;

namespace Test.FSM
{
    public class GameStateMachineInitializer : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;

        private readonly PreGameState _preGameState;
        private readonly GameState _gameState;

        public GameStateMachineInitializer(IGameStateMachine stateMachine, PreGameState preGameState, GameState gameState)
        {
            _stateMachine = stateMachine;
            _preGameState = preGameState;
            _gameState = gameState;
        }

        public void Initialize()
        {
            _stateMachine.RegisterStates(_preGameState, _gameState);

            _stateMachine.EnterState<PreGameState>();
        }
    }
}