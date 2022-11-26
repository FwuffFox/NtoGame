using System;
using System.Collections.Generic;
using GameScripts.Infrastructure.States;

namespace GameScripts.Services.Factories
{
    public interface IStateFactory
    {
        public Dictionary<Type, IStateWithExit> CreateStates();
    }
}