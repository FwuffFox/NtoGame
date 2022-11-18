using System;
using System.Collections.Generic;
using Infrastructure.States;
using Services.Factories;

namespace Infrastructure
{
    public class GameStateMachine
    {
        private readonly IStateFactory _stateFactory;
        private Dictionary<Type, IStateWithExit> _states;
        private IStateWithExit _currentState;

        public GameStateMachine(IStateFactory stateFactory) => 
            _stateFactory = stateFactory;

        public void CreateStates()
        {
            Dictionary<Type, IStateWithExit> states = _stateFactory.CreateStates();
            _states = states;
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = LoadState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IStateWithPayload<TPayload>
        {
            TState state = LoadState<TState>();
            state.Enter(payload);
        }

        private TState LoadState<TState>() where TState : class, IStateWithExit
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IStateWithExit => 
            _states[typeof(TState)] as TState;
    }
}