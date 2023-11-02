using System;
using System.Collections.Generic;
using Test.FSM.States;
using VContainer.Unity;

namespace Test.FSM
{
    public interface IGameStateMachine
    {
        IGameState CurrentState { get; }

        void RegisterStates(params IGameState[] states);

        void EnterState<T>() where T : IGameState;
    }

    public class GameStateMachine : IGameStateMachine, ITickable
    {
        public float rnd;
        public IGameState CurrentState { get; private set; }

        private readonly Dictionary<Type, IGameState> _statesMap = new Dictionary<Type, IGameState>();

        public GameStateMachine()
        {
            rnd = UnityEngine.Random.value;
        }
        public void RegisterStates(params IGameState[] states)
        {
            foreach (var state in states)
            {
                _statesMap[state.GetType()] = state;
            }
        }

        public void EnterState<T>() where T : IGameState
        {
            var state = _statesMap[typeof(T)];
            SwitchState(state);
        }

        private void SwitchState(IGameState state)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            CurrentState = state;
            CurrentState.Enter();
        }

        public void Tick()
        {
            if (CurrentState != null && CurrentState is IStateUpdate state)
                state.Update();
        }
    }
}