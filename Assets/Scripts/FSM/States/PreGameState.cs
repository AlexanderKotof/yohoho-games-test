using Test.UI;

namespace Test.FSM.States
{
    public class PreGameState : IGameState
    {
        public IGameStateMachine FSM { get; private set; }

        private UIManager _uiManager;

        public PreGameState(IGameStateMachine fsm, UIManager uiManager)
        {
            FSM = fsm;
            _uiManager = uiManager;
        }

        public void Enter()
        {
            // Show start screen
            // on button press => start game
            _uiManager.ShowStartGameScreen(() => FSM.EnterState<GameState>());
        }

        public void Exit()
        {
            _uiManager.HideStartScreen();
        }
    }
}