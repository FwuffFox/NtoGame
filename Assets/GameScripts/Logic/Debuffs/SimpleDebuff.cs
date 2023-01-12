using System;
using UnityEngine;

namespace GameScripts.Logic.Debuffs
{
    /// <summary>
    /// A debuff that is applying an effect each second.
    /// </summary>
    /// <typeparam name="TPlayerComponent">Component that will be changed during debuff.</typeparam>
    public class SimpleDebuff<TPlayerComponent> where TPlayerComponent : MonoBehaviour
    {
        public int Duration;
        
        public readonly Action<TPlayerComponent> ActionOnDebuffStart;

        public readonly Action<TPlayerComponent> ActionOnDebuffEnd;
        
        public SimpleDebuff(int duration, Action<TPlayerComponent> actionOnDebuffStart, Action<TPlayerComponent> actionOnDebuffEnd)
        {
            Duration = duration;
            ActionOnDebuffStart = actionOnDebuffStart;
            ActionOnDebuffEnd = actionOnDebuffEnd;
        }
    }
}