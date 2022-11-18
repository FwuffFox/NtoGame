using System;
using System.Collections.Generic;
using Infrastructure.States;

namespace Services.Factories
{
    public interface IStateFactory
    {
        public Dictionary<Type, IStateWithExit> CreateStates();
    }
}