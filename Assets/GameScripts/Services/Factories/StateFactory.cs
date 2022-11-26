using System;
using System.Collections.Generic;
using GameScripts.Extensions;
using GameScripts.Infrastructure.States;
using Zenject;

namespace GameScripts.Services.Factories
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer diContainer) =>
            _container = diContainer;

        public Dictionary<Type, IStateWithExit> CreateStates() =>
            new()
            {
                { typeof(BootstrapState), BindState(new BootstrapState()) },
                { typeof(LoadLevelState), BindState(new LoadLevelState()) },
                { typeof(GameLoopState), BindState(new GameLoopState()) }
            };

        private IStateWithExit BindState<T>(T state) where T : class, IStateWithExit =>
            state
                .With(_ =>  _container.BindInstance(state).AsSingle())
                .With(_ => _container.Inject(state));
    }
}